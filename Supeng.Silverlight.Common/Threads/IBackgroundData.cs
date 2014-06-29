using System;

namespace Supeng.Silverlight.Common.Threads
{
  public interface IBackgroundData<in T>
  {
    void BeginExecute();

    void EndExecute(T result);

    void CancelExecute();

    void HandleBackgroundException(Exception[] exceptions);
  }
}