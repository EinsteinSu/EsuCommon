using System;
using System.IO;
using Supeng.Common.IOs;

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

    public static void LogException(this Exception ex)
    {
      try
      {
        DateTime now = DateTime.Now;
        string logpath = string.Format(@"{0}{1}{2}{3}.log", DirectoryHelper.LogDirectory, now.Year, now.Month,
                                       now.Day);
        if (!File.Exists(logpath))
          File.Create(logpath).Close();
        File.AppendAllText(logpath,
                           string.Format("\r\n----------------------{0}--------------------------\r\n",
                                         now.ToString("yyyy-MM-dd HH:mm:ss")));
        File.AppendAllText(logpath, "=" + ex.Message + "=" + Environment.NewLine);
        File.AppendAllText(logpath, "=================================================" + Environment.NewLine);
        File.AppendAllText(logpath, "=" + ex.StackTrace + "=" + Environment.NewLine);
        File.AppendAllText(logpath, "=================================================" + Environment.NewLine);
      }
      catch 
      {
        Console.WriteLine("save log has some issue.");
      }
    }
  }
}
