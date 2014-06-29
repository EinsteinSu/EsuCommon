using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common.Tests
{
  /// <summary>
  ///   Interaction logic for EsuStepProgressTest.xaml
  /// </summary>
  public partial class EsuStepProgressTest : UserControl
  {
    public EsuStepProgressTest()
    {
      InitializeComponent();

      var model = new EsuStepProgressViewModel(100);
      DataContext = model;

      Task task = Task.Factory.StartNew(() =>
      {
        for (int i = 0; i < 100; i++)
        {
          model.StepAdd();
          Thread.Sleep(10);
        }
      });
      task.ContinueWith(t => { MessageBox.Show(model.Step.ToString()); });
    }
  }
}