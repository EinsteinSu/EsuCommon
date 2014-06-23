using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Interfaces;
using Supeng.Common.IOs;
using Update;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for UpdateTest.xaml
  /// </summary>
  public partial class UpdateTest : UserControl
  {
    public UpdateTest()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      UpdateHelper.ShowUpdate("test", new TestUpgradeData());
    }
  }

  public class TestUpgradeData : IUpgrade
  {
    string filePath = @"C:\Users\Einstein\Desktop\EsuCommon\Update\bin\Debug";
    public EsuUpgradeInfoCollection GetServiceFileCollection(string directoryName)
    {
      var collection = new EsuUpgradeInfoCollection();
      foreach (var file in Directory.GetFiles(filePath))
      {
        var info = new FileInfo(file);
        collection.Add(new EsuUpgradeInfo(filePath)
        {
          Type = FileType.File,
          Name = info.Name,
          FileName = file,
          LastWriteTime = info.LastWriteTime,
          Size = Math.Round(info.Length / 1024D, 2).ToString(CultureInfo.InvariantCulture)
        });
      }
      return collection;
    }

    public EsuUpgradeInfoCollection GetLocalFileCollection(string path)
    {
      return new EsuUpgradeInfoCollection();
    }

    public byte[] GetFileBytes(string fileName)
    {
      return string.Format("{0}\\{1}", filePath, fileName).FileToByte();
    }
  }
}
