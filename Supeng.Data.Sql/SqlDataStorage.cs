using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

namespace Supeng.Data.Sql
{
  public class SqlDataStorage : DataStorageBase
  {
    private readonly string connectionString;

    public SqlDataStorage(string connectionString, TaskScheduler scheduler = null)
      : base(connectionString, scheduler)
    {
      this.connectionString = connectionString;
    }

    public override int Execute(string sql, IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null)
    {
      var conn = new SqlConnection(connectionString);
      conn.Open();
      try
      {
        var command = new SqlCommand(sql, conn);
        if (parameters != null && parameters.Any())
        {
          command.CommandType = CommandType.StoredProcedure;
          foreach (var parameter in parameters)
            command.Parameters.Add(parameter);
        }
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
        conn.Close();
        conn.Dispose();
      }
    }

    public override void ExecuteWithAPM(string sql, IBackgroundData<int> backgroundData, IDataParameter[] parameters = null)
    {
      backgroundData.BeginExecute();
      var conn = new SqlConnection(connectionString);
      conn.Open();
      var command = new SqlCommand(sql, conn);
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
          conn.Close();
          conn.Dispose();
        }
      }), command);
    }

    public override EsuInfoCollection<T> ReadToCollection<T>(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null)
    {
      var conn = new SqlConnection(connectionString);
      conn.Open();
      var collection = new EsuInfoCollection<T>();
      try
      {
        CommandType type = parameters == null
          ? CommandType.Text
          : CommandType.StoredProcedure;
        var command = new SqlCommand(sql, conn) { CommandType = type };
        if (parameters != null)
        {
          foreach (IDataParameter parameter in parameters)
            command.Parameters.Add(parameter);
        }
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          if (Cancellation.Token.IsCancellationRequested)
            break;
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
        conn.Close();
        conn.Dispose();
      }
      return collection;
    }

    public override void ReadToCollectionWithAPM<T>(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData)
    {
      backgroundData.BeginExecute();
      var conn = new SqlConnection(connectionString);
      conn.Open();
      CommandType type = parameters == null
        ? CommandType.Text
        : CommandType.StoredProcedure;
      var command = new SqlCommand(sql, conn) { CommandType = type };
      command.BeginExecuteReader(ThreadHelper.SyncContextCallback(ar =>
      {
        try
        {
          SqlDataReader reader = command.EndExecuteReader(ar);
          var collection = new EsuInfoCollection<T>();
          while (reader.Read())
          {
            if (Cancellation.Token.IsCancellationRequested)
              break;
            collection.Add(dataCreator.CreateData(reader));
            Thread.Sleep(1000);
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
          conn.Close();
          conn.Dispose();
        }
      }), command);
    }
  }
}