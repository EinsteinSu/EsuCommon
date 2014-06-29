using System;
using System.Globalization;
using System.IO;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Interfaces;
using Supeng.Common.IOs;
using Update;

namespace Supeng.Update.Tests
{
  public class TestUpdate
  {
    public static void Main(string[] args)
    {
      UpdateHelper.ShowUpdate("test", new TestUpgradeData());
      Console.Read();
    }

    public static void Upgrade()
    {
      var viewModel = new UpdateWindowViewModel("", new TestUpgradeData());
      viewModel.Update();
    }
  }

  public class TestUpgradeData : IUpgrade
  {
    private string filePath = @"C:\Users\Einstein\Desktop\EsuCommon\Update\bin\Debug";

    public EsuUpgradeInfoCollection GetServiceFileCollection()
    {
      var collection = new EsuUpgradeInfoCollection();
      foreach (string file in Directory.GetFiles(filePath))
      {
        var info = new FileInfo(file);
        collection.Add(new EsuUpgradeInfo(filePath)
        {
          Type = FileType.File,
          Name = info.Name,
          FileName = file,
          LastWriteTime = info.LastWriteTime,
          Size = Math.Round(info.Length/1024D, 2).ToString(CultureInfo.InvariantCulture)
        });
      }
      return collection;
    }

    public EsuUpgradeInfoCollection GetLocalFileCollection()
    {
      return new EsuUpgradeInfoCollection();
    }

    public byte[] GetFileBytes(string fileName)
    {
      return string.Format("{0}\\{1}", filePath, fileName).FileToByte();
    }
  }
}