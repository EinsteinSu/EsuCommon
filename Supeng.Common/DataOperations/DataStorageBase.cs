using System;
using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

namespace Supeng.Common.DataOperations
{
  public abstract class DataStorageBase
  {
    protected readonly string ConnectionString;
    protected readonly TaskScheduler Scheduler;
    private readonly CancellationTokenSource cancellation;

    protected DataStorageBase(string connectionString, TaskScheduler scheduler = null)
    {
      ConnectionString = connectionString;
      cancellation = new CancellationTokenSource();
      Scheduler = scheduler ?? TaskScheduler.FromCurrentSynchronizationContext();
    }

    public CancellationTokenSource Cancellation
    {
      get { return cancellation; }
    }

    public abstract int Execute(string sql, IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null);

    public void ExecuteInBackground(string sql, IBackgroundData<int> backgroundData, IDataParameter[] parameters = null)
    {
      backgroundData.BeginExecute();
      var task = new Task<int>(() => Execute(sql, parameters), cancellation.Token);
      task.Start();
      task.HandleTaskResult(Scheduler, backgroundData);
    }

    /// <summary>
    /// execute sql script with APM
    /// </summary>
    /// <param name="sql">sql script</param>
    /// <param name="backgroundData">How to handle the exceptions with APM</param>
    public virtual void ExecuteWithAPM(string sql, IBackgroundData<int> backgroundData, IDataParameter[] parameters = null)
    {
      Func<string, int> func = s => Execute(s, parameters);
      backgroundData.BeginExecute();
      func.BeginInvoke(sql, ar =>
      {
        try
        {
          var f = (Func<string, int>)ar.AsyncState;
          int result = f.EndInvoke(ar);
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
      }, func);
    }

    public abstract EsuInfoCollection<T> ReadToCollection<T>(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null) where T : EsuInfoBase, new();

    public void ReadToCollectionInBackground<T>(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData) where T : EsuInfoBase, new()
    {
      backgroundData.BeginExecute();
      var task = new Task<EsuInfoCollection<T>>(() => ReadToCollection(sql, dataCreator, parameters), cancellation.Token);
      task.Start();
      task.HandleTaskResult(Scheduler, backgroundData);
    }

    public virtual void ReadToCollectionWithAPM<T>(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData) where T : EsuInfoBase, new()
    {
      Func<string, IDataCreator<T>, IDataParameter[], EsuInfoCollection<T>> func =
        (s, creator, p) => ReadToCollection(s, creator, p);
      backgroundData.BeginExecute();
      func.BeginInvoke(sql, dataCreator, parameters, ar =>
      {
        try
        {
          var f = (Func<string, IDataCreator<T>, IDataParameter[], EsuInfoCollection<T>>)ar.AsyncState;
          var collection = f.EndInvoke(ar);
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
      }, func);
    }
  }

  public enum ExecuteType
  {
    Normal, Background, WithAPM
  }
}