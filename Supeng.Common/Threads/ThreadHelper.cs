using System;
using System.Threading;

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
  }
}