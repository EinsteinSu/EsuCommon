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
  /// Interaction logic for MemoTextEditWindowTest.xaml
  /// </summary>
  public partial class MemoTextEditWindowTest : Window
  {
    public MemoTextEditWindowTest()
    {
      InitializeComponent();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      var viewModel = new MemoEditWindowViewModel("Test","");
      viewModel.ShowDialogWindow();
    }
  }
}
