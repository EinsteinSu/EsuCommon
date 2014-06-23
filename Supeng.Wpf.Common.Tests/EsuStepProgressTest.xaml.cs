using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
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
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  /// Interaction logic for EsuStepProgressTest.xaml
  /// </summary>
  public partial class EsuStepProgressTest : UserControl
  {
    public EsuStepProgressTest()
    {
      InitializeComponent();

      var model = new EsuStepProgressViewModel(100);
      DataContext = model;

      var task = Task.Factory.StartNew(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          model.StepAdd();
          Thread.Sleep(10);
        }
      });
      task.ContinueWith((t) =>
      {
        MessageBox.Show(model.Step.ToString());
      });
    }
  }
}
