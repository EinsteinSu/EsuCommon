﻿using System;
using System.IO;
using System.Reflection;

namespace Supeng.Common.IOs
{
  public static class DirectoryHelper
  {
    public static string CurrentDirectory
    {
      get
      {
        var item = Assembly.GetEntryAssembly();
        if (item == null)
        {
          return Environment.CurrentDirectory;
        }
        var location = item.Location;
        return Path.GetDirectoryName(location);
      }
    }
    public static string DataDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Data\\", CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string ImageDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Images\\", CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string TemplateDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Templates\\", CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    public static string TempDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Temps\\", CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }

    /// <summary>
    /// get a temp files with guid name
    /// </summary>
    /// <param name="extendName">extend name(.doc)</param>
    /// <returns></returns>
    public static string GetTempFile(string extendName)
    {
      return string.Format("{0}{1}{2}", TempDirectory, Guid.NewGuid(), extendName);
    }

    public static string LogDirectory
    {
      get
      {
        string dataDirectory = string.Format("{0}\\Logs\\", CurrentDirectory);
        if (!Directory.Exists(dataDirectory))
          Directory.CreateDirectory(dataDirectory);
        return dataDirectory;
      }
    }
  }
}