using System;
using System.Text;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

namespace Supeng.Common.DataOperations
{
  public static class DataSaveExtensions
  {
    public static void Save<T>(this ChangesCollection<T> collection, DataStorageBase storage, IDataSave<T> dataSave,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IDataSaveLog<T> log = null, IExceptionHandle handleException = null, Action startSave = null,
      Action<ChangeData<T>> doSave = null, Action allDone = null)
      where T : EsuInfoBase, new()
    {
      var sb = new StringBuilder();
      if (startSave != null)
        startSave();
      foreach (var change in collection)
      {
        switch (change.State)
        {
          case EsuDataState.Added:
            sb.AppendLine(dataSave.InsertSqlScript(change.Data) + ";");
            break;
          case EsuDataState.Modified:
            sb.AppendLine(dataSave.UpdateSqlScript(change.Data) + ";");
            break;
          case EsuDataState.Deleted:
            sb.AppendLine(dataSave.DeleteSqlScript(change.Data) + ";");
            break;
        }
        if (log != null)
          sb.AppendLine(log.DataOperationLog(change, userID) + ";");
        if (doSave != null)
          doSave(change);
        if (sb.Length > 1000)
        {
          ExecuteSqlScriptByType(sb.ToString(), storage, type, handleException);
          sb = new StringBuilder();
        }
      }
      if (!string.IsNullOrEmpty(sb.ToString()))
        ExecuteSqlScriptByType(sb.ToString(), storage, type, handleException);
      if (allDone != null)
        allDone();
    }

    public static void Save<T>(this EsuInfoCollection<T> collection, DataStorageBase storage, IDataSave<T> dataSave,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IDataSaveLog<T> log = null, IExceptionHandle handleException = null, Action startSave = null,
      Action<ChangeData<T>> doSave = null, Action allDone = null)
      where T : EsuInfoBase, new()
    {
      var sb = new StringBuilder();
      if (startSave != null)
        startSave();
      foreach (var change in collection.ChangedCollection)
      {
        switch (change.State)
        {
          case EsuDataState.Added:
            sb.AppendLine(dataSave.InsertSqlScript(change.Data) + ";");
            break;
          case EsuDataState.Modified:
            sb.AppendLine(dataSave.UpdateSqlScript(change.Data) + ";");
            break;
          case EsuDataState.Deleted:
            sb.AppendLine(dataSave.DeleteSqlScript(change.Data) + ";");
            break;
        }
        if (log != null)
          sb.AppendLine(log.DataOperationLog(change, userID) + ";");
        if (doSave != null)
          doSave(change);
        if (sb.Length > 1000)
        {
          ExecuteSqlScriptByType(sb.ToString(), storage, type, handleException);
          sb = new StringBuilder();
        }
      }
      if (!string.IsNullOrEmpty(sb.ToString()))
        ExecuteSqlScriptByType(sb.ToString(), storage, type, handleException);
      collection.AcceptChanges();
      if (allDone != null)
        allDone();
    }

    public static void SaveSingleRecord<T>(this T data, EsuDataState state, DataStorageBase storage, IDataSave<T> dataSave,
      IDataSaveLog<T> log = null, ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      StringBuilder sb = new StringBuilder();
      if (startSave != null)
        startSave();
      switch (state)
      {
        case EsuDataState.Added:
          sb.AppendLine(dataSave.InsertSqlScript(data));
          break;
        case EsuDataState.Modified:
          sb.AppendLine(dataSave.UpdateSqlScript(data));
          break;
        case EsuDataState.Deleted:
          sb.AppendLine(dataSave.DeleteSqlScript(data));
          break;
      }
      if (log != null)
        sb.AppendLine(log.DataOperationLog(new ChangeData<T> { State = state, Data = data, ChangeTime = DateTime.Now }, userID) + ";");
      if (!string.IsNullOrEmpty(sb.ToString()))
        ExecuteSqlScriptByType(sb.ToString(), storage, type, handleException);
      if (allDone != null)
        allDone();
    }

    public static void Insert<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null, ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Added, storage, dataSave, log, type, userID, handleException, startSave, allDone);
    }

    public static void Update<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null, ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Modified, storage, dataSave, log, type, userID, handleException, startSave, allDone);
    }

    public static void Delete<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null, ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Deleted, storage, dataSave, log, type, userID, handleException, startSave, allDone);
    }

    private static void ExecuteSqlScriptByType(string sql, DataStorageBase storage, ExecuteType type, IExceptionHandle handleException)
    {
      switch (type)
      {
        case ExecuteType.Normal:
          storage.Execute(sql, exceptionHandle: handleException);
          break;
        case ExecuteType.Background:
          storage.ExecuteInBackground(sql, handleException as IBackgroundData<int>);
          break;
        case ExecuteType.WithAPM:
          storage.ExecuteWithAPM(sql, handleException as IBackgroundData<int>);
          break;
      }
    }
  }
}