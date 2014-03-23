using System;
using System.Collections.Generic;

namespace Supeng.Common.Threads
{
  public interface IBackgroundData<in T>
  {
    void BeginExecute();

    void EndExecute(T result);

    void CancelExecute();

    void HandleException(Exception[] exceptions);
  }
}
