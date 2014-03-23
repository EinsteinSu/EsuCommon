using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Threads;

namespace Supeng.Common.DataOperations
{
  public abstract class DataStorageBase<T> where T : EsuInfoBase, new()
  {
    protected readonly string ConnectionString;
    protected readonly TaskScheduler Scheduler;
    private readonly CancellationTokenSource cancellation;

    protected DataStorageBase(string connectionString, TaskScheduler scheduler = null)
    {
      ConnectionString = connectionString;
      cancellation = new CancellationTokenSource();
      Scheduler = scheduler ?? TaskScheduler.Current;
    }

    public CancellationTokenSource Cancellation
    {
      get { return cancellation; }
    }

    public abstract int Execute(string sql);

    public void ExecuteInBackground(string sql, IBackgroundData<int> backgroundData)
    {
      var task = new Task<int>(() => Execute(sql), cancellation.Token);
      task.Start();

      task.HandleTaskResult(cancellation, Scheduler, backgroundData);
    }

    public abstract EsuInfoCollection<T> ReadToCollection(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null);

    public void ReadToCollectionInBackground(string sql, IDataCreator<T> dataCreator,
      IBackgroundData<EsuInfoCollection<T>> backgroundData, IDataParameter[] parameters = null)
    {
      var task = new Task<EsuInfoCollection<T>>(() => ReadToCollection(sql, dataCreator, parameters), cancellation.Token);
      task.Start();

      task.HandleTaskResult(cancellation, Scheduler, backgroundData);
    }
  }
}