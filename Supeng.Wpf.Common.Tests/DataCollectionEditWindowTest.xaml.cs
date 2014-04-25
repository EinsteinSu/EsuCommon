using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Supeng.Common.Strings;
using Supeng.Data;
using Supeng.Wpf.Common.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for DataCollectionEditWindowTest.xaml
  /// </summary>
  public partial class DataCollectionEditWindowTest : Window
  {
    public DataCollectionEditWindowTest()
    {
      InitializeComponent();

      this.Content = ToolbarWithContentHelper.GetCollectionEditControl(new CollectionEditWithWindow());
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
