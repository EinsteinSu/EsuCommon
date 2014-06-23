using System;
using System.Globalization;
using System.IO;

namespace UpgradeToolKit
{
  public static class DirectoryHelper
  {
    private static EsuFileInfoCollection _files;
    private static string _root;

    public static EsuFileInfoCollection GetFilesFromDirectory(this string path, bool getDirectory = false)
    {
      _files = new EsuFileInfoCollection();
      _root = path;
      GetFiles(path, getDirectory);
      return _files;
    }

    private static void GetFiles(string path, bool getDirectory)
    {
      foreach (string file in Directory.GetFiles(path))
      {
        var info = new FileInfo(file);
        _files.Add(new EsuFileInfo(_root)
        {
          Type = FileType.File,
          Name = info.Name,
          FileName = file,
          LastWriteTime = info.LastWriteTime,
          Size = Math.Round(info.Length/1024D, 2).ToString(CultureInfo.InvariantCulture)
        });
      }
      foreach (string directory in Directory.GetDirectories(path))
      {
        if (getDirectory)
        {
          var directoryInfo = new DirectoryInfo(directory);
          _files.Add(new EsuFileInfo(_root)
          {
            Type = FileType.Directory,
            FileName = directory,
            Name = directoryInfo.Name
          });
        }
        GetFiles(directory, getDirectory);
      }
    }
  }
}