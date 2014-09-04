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
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for TreeEditTestWindow.xaml
  /// </summary>
  public partial class TreeEditTestWindow : Window
  {
    public TreeEditTestWindow()
    {
      InitializeComponent();

      Loaded += TreeEditTestWindow_Loaded;
     
    }

    void TreeEditTestWindow_Loaded(object sender, RoutedEventArgs e)
    {
      var viewModel = new TreeEditTestWindowViewModel();
      Content = ToolbarWithContentHelper.GetToolbarWithContentControl(viewModel);
      viewModel.Load();
      viewModel.Data.CurrentItemChanged = data =>
      {
        MessageBox.Show(data.Name);
      };
      
    }
  }

  public class TreeEditTestWindowViewModel : TreeCollectionEditViewModel<TestTreeData>
  {
    protected override string EntityName
    {
      get { return "Test tree data"; }
    }

    protected override EsuInfoCollection<TestTreeData> LoadData()
    {
      return new EsuInfoCollection<TestTreeData>();
    }

    public override void Save()
    {
      MessageBox.Show("Save");
    }
  }
}
