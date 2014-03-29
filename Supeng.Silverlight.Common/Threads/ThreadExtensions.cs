using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Supeng.Silverlight.Common.Threads
{
  public static class ThreadExtensions
  {
    public static void HandleTaskResult<T>(this Task<T> task, TaskScheduler scheduler, IBackgroundData<T> backgroundData)
    {
      task.ContinueWith(t => backgroundData.EndExecute(t.Result), CancellationToken.None,
        TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);

      task.ContinueWith(t => backgroundData.CancelExecute(), CancellationToken.None,
        TaskContinuationOptions.OnlyOnCanceled, scheduler);

      task.ContinueWith(t =>
      {
        if (task.Exception != null)
          backgroundData.HandleBackgroundException(task.Exception.InnerExceptions.ToArray());
      }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, scheduler);
    }
  }
}