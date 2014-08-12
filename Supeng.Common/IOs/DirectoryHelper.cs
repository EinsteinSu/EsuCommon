using System;
using System.IO;

namespace Supeng.Common.IOs
{
  public static class DirectoryHelper
  {
    public static string DataDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Data\\", Environment.CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string ImageDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Images\\", Environment.CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string TemplateDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Templates\\", Environment.CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string TempDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Temps\\", Environment.CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string LogDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Logs\\", Environment.CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }
  }
}