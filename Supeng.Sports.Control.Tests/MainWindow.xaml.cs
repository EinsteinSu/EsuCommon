using System.Windows;
using Supeng.Sports.Control.ViewModels;

namespace Supeng.Sports.Control.Tests
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();

      var viewModel = new EsuDesTimerViewModel();
      viewModel.SetTime(0, 1, 0);
      DataContext = viewModel;
    }
  }
}