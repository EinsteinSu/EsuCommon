using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Supeng.Common.Threads
{
  public static class ThreadExtensions
  {
    public static void HandleTaskResult<T>(this Task<T> task, CancellationTokenSource tokenSource,
      TaskScheduler scheduler, IBackgroundData<T> backgroundData)
    {
      task.ContinueWith(t => backgroundData.EndExecute(t.Result), tokenSource.Token,
        TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);

      task.ContinueWith(t => backgroundData.CancelExecute(), TaskContinuationOptions.OnlyOnCanceled);

      task.ContinueWith(t =>
      {
        if (task.Exception != null)
          backgroundData.HandleException(task.Exception.InnerExceptions.ToArray());
      }, TaskContinuationOptions.OnlyOnFaulted);
    }
  }
}
