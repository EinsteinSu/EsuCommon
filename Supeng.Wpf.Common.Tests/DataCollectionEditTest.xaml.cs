using System.Globalization;
using System.Windows;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Data;
using Supeng.Wpf.Common.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for DataCollectionEditTest.xaml
  /// </summary>
  public partial class DataCollectionEditTest
  {
    public DataCollectionEditTest()
    {
      InitializeComponent();

      Content = ToolbarWithContentHelper.GetCollectionEditControl(new TestDataEditViewModel());
    }
  }

  internal sealed class TestDataEditViewModel : CollectionEditViewModel<TestData>
  {
    public override string EntityName
    {
      get { return "TestData"; }
    }

    protected override EsuDataEditCollection<TestData> GetData()
    {
      return new TestDataEditCollection(null);
    }

    public override void Save()
    {
    }
  }

  internal sealed class TestDataEditCollection : EsuDataEditCollection<TestData>
  {
    public TestDataEditCollection(DataStorageBase storage)
      : base(storage)
    {
    }

    protected override void LoadCollection(DataStorageBase storage)
    {
      Collection = new EsuInfoCollection<TestData>();
      for (int i = 0; i < 10; i++)
      {
        Collection.Add(new TestData {ID = i.ToString(CultureInfo.InvariantCulture), Description = "Items:" + i});
      }
    }

    public override void Save()
    {
      MessageBox.Show("Save success!");
    }

    protected override void CurrentItemChanged(TestData current)
    {
    }
  }
}