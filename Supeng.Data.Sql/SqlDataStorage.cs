using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Data.Sql
{
  public class SqlDataStorage<T> : DataStorageBase<T> where T : EsuInfoBase, new()
  {
    protected SqlConnection Connection;
    public SqlDataStorage(string connectionString)
      : base(connectionString)
    {
      Connection = new SqlConnection(connectionString);
    }

    public override int Execute(string sql)
    {
      Connection.Open();
      try
      {
        var command = new SqlCommand(sql, Connection);
        return command.ExecuteNonQuery();
      }
      catch (SqlException exception)
      {
        //add logs to local file or send to service
        throw new Exception(exception.Message);
      }
      finally
      {
        if (Connection != null)
          Connection.Close();
      }
    }

    public override EsuInfoCollection<T> ReadToCollection(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null)
    {
      Connection.Open();
      try
      {
        var collection = new EsuInfoCollection<T>();
        CommandType type = parameters == null
          ? CommandType.Text
          : CommandType.StoredProcedure;
        SqlCommand command = new SqlCommand(sql, Connection) { CommandType = type };
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          collection.Add(dataCreator.CreateData(reader));
        }
        return collection;
      }
      catch (Exception exception)
      {
        //add logs to local file or send to service
        throw new Exception(exception.Message);
      }
      finally
      {
        if (Connection != null)
          Connection.Close();
      }
    }
  }
}
