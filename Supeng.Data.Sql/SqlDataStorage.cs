using System;
using System.Data;
using System.Data.SqlClient;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

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

    public override int Execute(string sql, IExceptionHandle exceptionHandle = null)
    {
      Connection.Open();
      try
      {
        var command = new SqlCommand(sql, Connection);
        return command.ExecuteNonQuery();
      }
      catch (SqlException exception)
      {
        if (exceptionHandle != null)
        {
          exceptionHandle.Handle(exception);
          return -1;
        }
        else
          throw new Exception(exception.Message);
      }
      finally
      {
        if (Connection != null)
          Connection.Close();
      }
    }

    public virtual void ExecuteWithAPM(string sql, IBackgroundData<int> backgroundData)
    {
      backgroundData.BeginExecute();
      Connection.Open();
      var command = new SqlCommand(sql, Connection);
      command.BeginExecuteNonQuery(ThreadHelper.SyncContextCallback(ar =>
      {
        try
        {
          int result = command.EndExecuteNonQuery(ar);
          backgroundData.EndExecute(result);
        }
        catch (Exception exception)
        {
          var exceptionHandle = backgroundData as IExceptionHandle;
          if (exceptionHandle != null)
            exceptionHandle.Handle(exception);
          else
          {
            throw new Exception(exception.Message);
          }
        }
        finally
        {
          if (Connection != null)
            Connection.Close();
        }
      }), command);
    }

    public override EsuInfoCollection<T> ReadToCollection(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null)
    {
      Connection.Open();
      var collection = new EsuInfoCollection<T>();
      try
      {
        CommandType type = parameters == null
          ? CommandType.Text
          : CommandType.StoredProcedure;
        var command = new SqlCommand(sql, Connection) {CommandType = type};
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          collection.Add(dataCreator.CreateData(reader));
        }
      }
      catch (Exception exception)
      {
        if (exceptionHandle != null)
          exceptionHandle.Handle(exception);
        else
        {
          throw new Exception(exception.Message);
        }
      }
      finally
      {
        if (Connection != null)
          Connection.Close();
      }
      return collection;
    }

    public virtual void ReadToCollectionWithAPM(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData)
    {
      backgroundData.BeginExecute();
      Connection.Open();
      CommandType type = parameters == null
        ? CommandType.Text
        : CommandType.StoredProcedure;
      var command = new SqlCommand(sql, Connection) {CommandType = type};
      command.BeginExecuteReader(ThreadHelper.SyncContextCallback(ar =>
      {
        try
        {
          SqlDataReader reader = command.EndExecuteReader(ar);
          var collection = new EsuInfoCollection<T>();
          while (reader.Read())
          {
            collection.Add(dataCreator.CreateData(reader));
          }
          backgroundData.EndExecute(collection);
        }
        catch (Exception exception)
        {
          var exceptionHandle = backgroundData as IExceptionHandle;
          if (exceptionHandle != null)
            exceptionHandle.Handle(exception);
          else
          {
            throw new Exception(exception.Message);
          }
        }
        finally
        {
          if (Connection != null)
            Connection.Close();
        }
      }), command);
    }
  }
}