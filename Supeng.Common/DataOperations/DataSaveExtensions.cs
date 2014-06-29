using System;
using System.Data;
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
      if (startSave != null)
        startSave();
      foreach (var change in collection)
      {
        switch (change.State)
        {
          case EsuDataState.Added:
            ExecuteSqlScriptByType(dataSave.InsertSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
          case EsuDataState.Modified:
            ExecuteSqlScriptByType(dataSave.UpdateSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
          case EsuDataState.Deleted:
            ExecuteSqlScriptByType(dataSave.DeleteSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
        }
        if (log != null)
          ExecuteSqlScriptByType(log.LogSqlScript(), log.MappingParameters(change, userID), storage, type,
            handleException);
        if (doSave != null)
          doSave(change);
      }
      if (allDone != null)
        allDone();
    }

    public static void Save<T>(this EsuInfoCollection<T> collection, DataStorageBase storage, IDataSave<T> dataSave,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IDataSaveLog<T> log = null, IExceptionHandle handleException = null, Action startSave = null,
      Action<ChangeData<T>> doSave = null, Action allDone = null)
      where T : EsuInfoBase, new()
    {
      if (startSave != null)
        startSave();
      foreach (var change in collection.ChangedCollection)
      {
        switch (change.State)
        {
          case EsuDataState.Added:
            ExecuteSqlScriptByType(dataSave.InsertSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
          case EsuDataState.Modified:
            ExecuteSqlScriptByType(dataSave.UpdateSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
          case EsuDataState.Deleted:
            ExecuteSqlScriptByType(dataSave.DeleteSqlScript(), dataSave.MappingParameters(change.Data), storage, type,
              handleException);
            break;
        }
        if (log != null)
          ExecuteSqlScriptByType(log.LogSqlScript(), log.MappingParameters(change, userID), storage, type,
            handleException);
        if (doSave != null)
          doSave(change);
      }
      collection.AcceptChanges();
      if (allDone != null)
        allDone();
    }

    public static void SaveSingleRecord<T>(this T data, EsuDataState state, DataStorageBase storage,
      IDataSave<T> dataSave,
      IDataSaveLog<T> log = null, ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      if (startSave != null)
        startSave();
      switch (state)
      {
        case EsuDataState.Added:
          ExecuteSqlScriptByType(dataSave.InsertSqlScript(), dataSave.MappingParameters(data), storage, type,
            handleException);
          break;
        case EsuDataState.Modified:
          ExecuteSqlScriptByType(dataSave.UpdateSqlScript(), dataSave.MappingParameters(data), storage, type,
            handleException);
          break;
        case EsuDataState.Deleted:
          ExecuteSqlScriptByType(dataSave.DeleteSqlScript(), dataSave.MappingParameters(data), storage, type,
            handleException);
          break;
      }
      if (log != null)

      {
        var change = new ChangeData<T> {State = state, Data = data, ChangeTime = DateTime.Now};
        ExecuteSqlScriptByType(log.LogSqlScript(), log.MappingParameters(change, userID), storage, type,
          handleException);
      }
      if (allDone != null)
        allDone();
    }

    public static void Insert<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Added, storage, dataSave, log, type, userID, handleException, startSave,
        allDone);
    }

    public static void Update<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Modified, storage, dataSave, log, type, userID, handleException, startSave,
        allDone);
    }

    public static void Delete<T>(this T data, DataStorageBase storage, IDataSave<T> dataSave, IDataSaveLog<T> log = null,
      ExecuteType type = ExecuteType.Normal, string userID = "System",
      IExceptionHandle handleException = null, Action startSave = null, Action allDone = null) where T : EsuInfoBase
    {
      SaveSingleRecord(data, EsuDataState.Deleted, storage, dataSave, log, type, userID, handleException, startSave,
        allDone);
    }

    private static void ExecuteSqlScriptByType(string sql, IDataParameter[] parameters, DataStorageBase storage,
      ExecuteType type, IExceptionHandle handleException)
    {
      switch (type)
      {
        case ExecuteType.Normal:
          storage.Execute(sql, parameters, handleException);
          break;
        case ExecuteType.Background:
          storage.ExecuteInBackground(sql, handleException as IBackgroundData<int>, parameters);
          break;
        case ExecuteType.WithAPM:
          storage.ExecuteWithAPM(sql, handleException as IBackgroundData<int>, parameters);
          break;
      }
    }
  }
}