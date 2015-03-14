using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
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

    protected virtual void ProcessCommandParameter(SqlCommand command, IDataParameter[] parameters)
    {
      if (parameters != null && parameters.Any())
      {
        foreach (IDataParameter parameter in parameters)
        {
          if (parameter.Value == null)
            parameter.Value = DBNull.Value;
          command.Parameters.Add(parameter);
        }
      }
    }

    public override int Execute(string sql, IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null,
      CommandType type = CommandType.Text)
    {
      var conn = new SqlConnection(connectionString);
      conn.Open();
      try
      {
        var command = new SqlCommand(sql, conn) { CommandType = type };
        ProcessCommandParameter(command, parameters);
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
      }
    }

    public override IDbCommand GetCommand(string sql, IDataParameter[] parameters = null, CommandType cmdType = CommandType.Text)
    {
      var command = new SqlCommand(sql) { CommandType = cmdType };
      if (parameters != null && parameters.Any())
      {
        foreach (IDataParameter parameter in parameters)
        {
          if (parameter.Value == null)
            parameter.Value = DBNull.Value;
          command.Parameters.Add(parameter);
        }
      }
      return command;
    }

    public override void ReadData(string sql, Action<IDataReader> todoAction, IDataParameter[] parameters = null, CommandType cmdType = CommandType.Text,
      IExceptionHandle exceptionHandle = null)
    {
      var conn = new SqlConnection(connectionString);
      conn.Open();
      try
      {
        var command = new SqlCommand(sql, conn) { CommandType = cmdType };
        ProcessCommandParameter(command, parameters);
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          if (Cancellation.Token.IsCancellationRequested)
            break;
          if (todoAction != null)
            todoAction(reader);
        }
      }
      catch (SqlException exception)
      {
        if (exceptionHandle != null)
        {
          exceptionHandle.Handle(exception);
        }
        else
          throw new Exception(exception.Message);
      }
      finally
      {
        conn.Close();
      }
    }

    public override void ExecuteWithAPM(string sql, IBackgroundData<int> backgroundData,
      IDataParameter[] parameters = null, CommandType type = CommandType.Text)
    {
      backgroundData.BeginExecute();
      var conn = new SqlConnection(connectionString);
      conn.Open();
      var command = new SqlCommand(sql, conn) { CommandType = type };
      if (parameters != null && parameters.Any())
      {
        foreach (IDataParameter parameter in parameters)
        {
          if (parameter.Value == null)
            parameter.Value = DBNull.Value;
          command.Parameters.Add(parameter);
        }
      }
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

    protected virtual CommandType GetCommandType(string sql, IDataParameter[] parameters)
    {
      if (sql.StartsWith("select", StringComparison.InvariantCultureIgnoreCase))
        return CommandType.Text;
      return parameters == null
          ? CommandType.Text
          : CommandType.StoredProcedure;
    }

    public override EsuInfoCollection<T> ReadToCollection<T>(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null)
    {
      var conn = new SqlConnection(connectionString);
      conn.Open();
      var collection = new EsuInfoCollection<T>();
      try
      {
        CommandType type = GetCommandType(sql, parameters);
        var command = new SqlCommand(sql, conn) { CommandType = type };
        if (parameters != null)
        {
          foreach (IDataParameter parameter in parameters)
          {
            if (parameter.Value == null)
              parameter.Value = DBNull.Value;
            command.Parameters.Add(parameter);
          }
        }
        SqlDataReader reader = command.ExecuteReader();
        while (reader.Read())
        {
          if (Cancellation.Token.IsCancellationRequested)
            break;
          collection.Add(dataCreator.CreateData(reader));
        }
        collection.AcceptChanges();
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
      }
      return collection;
    }

    public override void ReadToCollectionWithAPM<T>(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData)
    {
      backgroundData.BeginExecute();
      var conn = new SqlConnection(connectionString);
      conn.Open();
      CommandType type = GetCommandType(sql, parameters);
      var command = new SqlCommand(sql, conn) { CommandType = type };
      if (parameters != null)
      {
        foreach (IDataParameter parameter in parameters)
        {
          if (parameter.Value == null)
            parameter.Value = DBNull.Value;
          command.Parameters.Add(parameter);
        }
      }
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
          }
          collection.AcceptChanges();
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