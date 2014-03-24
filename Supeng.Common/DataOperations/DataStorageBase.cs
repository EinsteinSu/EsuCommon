using System.Data;
using System.Threading;
using System.Threading.Tasks;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Exceptions;
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

    public abstract int Execute(string sql, IExceptionHandle exceptionHandle = null);

    public void ExecuteInBackground(string sql, IBackgroundData<int> backgroundData)
    {
      var task = new Task<int>(() => Execute(sql), cancellation.Token);
      task.Start();

      task.HandleTaskResult(Scheduler, backgroundData);
    }

    public abstract EsuInfoCollection<T> ReadToCollection(string sql, IDataCreator<T> dataCreator,
      IDataParameter[] parameters = null, IExceptionHandle exceptionHandle = null);

    public void ReadToCollectionInBackground(string sql, IDataCreator<T> dataCreator, IDataParameter[] parameters,
      IBackgroundData<EsuInfoCollection<T>> backgroundData)
    {
      var task = new Task<EsuInfoCollection<T>>(() => ReadToCollection(sql, dataCreator, parameters), cancellation.Token);
      task.Start();

      task.HandleTaskResult(Scheduler, backgroundData);
    }
  }
}