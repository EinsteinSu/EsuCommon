using System;
using System.Globalization;
using System.IO;

namespace UpdateToolkit
{
  public static class EsuUpgradeInfoHelper
  {
    public static EsuUpgradeInfoCollection GetUpgradeCollection(string directory)
    {
      var collection = new EsuUpgradeInfoCollection();

      #region get all directory

      foreach (string dir in Directory.GetDirectories(directory, "*", SearchOption.AllDirectories))
      {
        var directoryInfo = new DirectoryInfo(dir);
        collection.Add(new EsuUpgradeInfo
        {
          Type = FileType.Directory,
          FileName = dir,
          Name = directoryInfo.Name,
          RelativeFileName = dir.Replace(directory, "")
        });
      }

      #endregion

      #region get all files

      foreach (string file in Directory.GetFiles(directory, "*", SearchOption.AllDirectories))
      {
        var info = new FileInfo(file);
        collection.Add(new EsuUpgradeInfo
        {
          Type = FileType.File,
          Name = info.Name,
          FileName = file,
          RelativeFileName = file.Replace(directory, ""),
          LastWriteTime = info.LastWriteTime,
          Size = Math.Round(info.Length/1024D, 2).ToString(CultureInfo.InvariantCulture)
        });
      }

      #endregion

      return collection;
    }
  }
}