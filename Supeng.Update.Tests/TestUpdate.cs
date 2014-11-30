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
      //UpdateHelper.ShowUpdate("test", new TestUpgradeData());
      Upgrade();
      Console.Read();
    }

    public static void Upgrade()
    {
      var viewModel = new UpdateWindowViewModel("", new TestUpgradeData(),"update.exe");
      viewModel.Update();
    }
  }

  public class TestUpgradeData : IUpgrade
  {
    private string filePath = @"C:\Users\Einstein\Desktop\EsuCommon\Update\bin\Debug";

    public EsuUpgradeInfoCollection GetServiceFileCollection()
    {
      return EsuUpgradeInfoHelper.GetUpgradeCollection(filePath);
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