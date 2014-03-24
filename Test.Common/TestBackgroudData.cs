using System;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;

namespace Test.Common
{
  public class TestBackgroudData : IBackgroundData<int>, IExceptionHandle
  {
    public void BeginExecute()
    {
      Console.WriteLine("Begin execute");
    }

    public void EndExecute(int result)
    {
      Console.WriteLine("Execute success the effect result has {0} records.", result);
    }

    public void CancelExecute()
    {
      Console.WriteLine("Execute has canceled.");
    }

    public void HandleBackgroundException(Exception[] exceptions)
    {
      foreach (var exception in exceptions)
      {
        Console.WriteLine(exception.Message);
      }
    }

    public void Handle(Exception ex)
    {
      Console.WriteLine(ex.Message);
    }
  }
}
