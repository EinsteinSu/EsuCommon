using System;

namespace Supeng.Common.Exceptions
{
  public static class ExceptionExtensions
  {
    public static string ProcessWithException<T>(Action doAction, string exceptionBegin = "保存错误:") where T : Exception
    {
      try
      {
        doAction();
        return string.Empty;
      }
      catch (T exception)
      {
        return exceptionBegin + exception.Message;
      }
    }
  }
}
