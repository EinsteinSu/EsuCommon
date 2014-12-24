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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Supeng.Common.Controls;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// CollectionSearchWithQueryTest.xaml 的交互逻辑
  /// </summary>
  public partial class CollectionSearchWithQueryTest : UserControl
  {
    public CollectionSearchWithQueryTest()
    {
      InitializeComponent();
      IProgress progress = new EsuProgressViewModel();
      Content =
        ToolbarWithContentHelper.GetToolbarWithContentControl(new CollectionSearchWithQueryTestViewModel(progress));
    }
  }

  public class CollectionSearchWithQueryTestViewModel : CollectionQueryWithSearchControlViewModel<TestData>
  {
    public CollectionSearchWithQueryTestViewModel(IProgress progress)
      : base(progress)
    {
    }

    protected override EsuInfoCollection<TestData> GetCollection()
    {
      return new EsuInfoCollection<TestData>();
    }

    public override SearchControlViewModel Search
    {
      get { return new SearchControlTestViewModel(); }
    }
  }
}
