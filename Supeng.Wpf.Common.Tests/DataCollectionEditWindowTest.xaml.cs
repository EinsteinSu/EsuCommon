using System.Windows;
using Supeng.Common.Strings;
using Supeng.Data;
using Supeng.Wpf.Common.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for DataCollectionEditWindowTest.xaml
  /// </summary>
  public partial class DataCollectionEditWindowTest : Window
  {
    public DataCollectionEditWindowTest()
    {
      InitializeComponent();

      Content = ToolbarWithContentHelper.GetCollectionEditControl(new CollectionEditWithWindow());
    }
  }

  internal class CollectionEditWithWindow : CollectionEditWithWindowViewModel<TestData, TestData>
  {
    public CollectionEditWithWindow()
      : base(new EntityEditViewModelBase<TestData>())
    {
    }

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

    protected override void Insert(TestData data)
    {
      MessageBox.Show("Insert");
    }

    protected override void Update(TestData data)
    {
      MessageBox.Show("Update");
    }

    protected override void Delete(TestData data)
    {
      MessageBox.Show("Delete");
    }

    protected override TestData ReadAfterChanged(TestData data)
    {
      return data.ToString().Load<TestData>();
    }
  }
}