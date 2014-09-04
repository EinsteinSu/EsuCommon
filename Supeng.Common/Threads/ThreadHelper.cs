using System;
using System.Threading;
using System.Threading.Tasks;

namespace Supeng.Common.Threads
{
  public static class ThreadHelper
  {
    public static AsyncCallback SyncContextCallback(AsyncCallback callback)
    {
      SynchronizationContext context = SynchronizationContext.Current;
      if (context == null)
        return callback;
      return asyncResult => context.Post(result => callback((IAsyncResult)result), asyncResult);
    }

    public static void DoTask<T>(Func<T> taskStart, Action<T> taskDone, Action<AggregateException> handleException,
      TaskScheduler scheduler = null)
    {
      if (scheduler == null)
        scheduler = TaskScheduler.FromCurrentSynchronizationContext();
      Task<T> task = Task<T>.Factory.StartNew(taskStart, CancellationToken.None, TaskCreationOptions.None, scheduler);
      task.ContinueWith(t => taskDone(t.Result), CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion,
        TaskScheduler.Current);
      task.ContinueWith(t =>
      {
        if (handleException != null)
          handleException(t.Exception);
      }, CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);
    }

    public static void DoTaskWithoutResult(Action taskStart, Action taskDone, Action<AggregateException> handleException,
      TaskScheduler scheduler = null)
    {
      if (scheduler == null)
        scheduler = TaskScheduler.FromCurrentSynchronizationContext();
      Task task = Task.Factory.StartNew(taskStart, CancellationToken.None, TaskCreationOptions.None, scheduler);
      task.ContinueWith(t => taskDone(), CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion,
        TaskScheduler.Current);
      task.ContinueWith(t =>
      {
        if (handleException != null)
          handleException(t.Exception);
      }, CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted, TaskScheduler.Current);
    }
  }
}