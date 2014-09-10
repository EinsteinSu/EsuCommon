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
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for MutipleSelectionWindowTest.xaml
  /// </summary>
  public partial class MutipleSelectionWindowTest : Window
  {
    private TestMutipleSelectionViewModel viewModel;
    public MutipleSelectionWindowTest()
    {
      InitializeComponent();

      viewModel = new TestMutipleSelectionViewModel();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var result = viewModel.ShowDialogWindowWithReturn();
      if (result)
        MessageBox.Show(viewModel.DataResult.ToString());
    }
  }

  public class TestMutipleSelectionViewModel : MultipleSelectionWindowBaseViewModel<string>
  {
    protected override IList<string> GetData()
    {
      var list = new List<string>();
      for (int i = 0; i < 10; i++)
      {
        list.Add("Item" + i);
      }
      return list;
    }
  }
}
