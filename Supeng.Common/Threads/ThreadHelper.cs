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
      return asyncResult => context.Post(result => callback((IAsyncResult) result), asyncResult);
    }

    public static void DoTask<T>(Func<T> taskStart, Action<T> taskDone, Action<AggregateException> handleException,
      TaskScheduler scheduler)
    {
      Task<T> task = Task<T>.Factory.StartNew(taskStart);
      task.ContinueWith(t => taskDone(t.Result), CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion,
        scheduler);
      task.ContinueWith(t =>
      {
        if (handleException != null)
          handleException(t.Exception);
      }, CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted, scheduler);
    }

    public static void DoTaskWithoutResult(Action taskStart, Action taskDone, Action<AggregateException> handleException,
      TaskScheduler scheduler)
    {
      Task task = Task.Factory.StartNew(taskStart);
      task.ContinueWith(t => taskDone(), CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion,
        scheduler);
      task.ContinueWith(t =>
      {
        if (handleException != null)
          handleException(t.Exception);
      }, CancellationToken.None,
        TaskContinuationOptions.OnlyOnFaulted, scheduler);
    }
  }
}