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
using Caliburn.Micro;
using Supeng.Common.Entities;
using Supeng.Sports.Common.Timers;

namespace Supeng.Sports.Common.Tests
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    private TestTimeViewModel viewModel;
    public MainWindow()
    {
      InitializeComponent();

      viewModel = new TestTimeViewModel();
      DataContext = viewModel;
    }

    private void StartClick(object sender, RoutedEventArgs e)
    {
      viewModel.Timers[11].Start();
    }

    private void StopClick(object sender, RoutedEventArgs e)
    {
      viewModel.Timers[11].Stop();
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
      viewModel.Timers[11].Reset();
    }
  }

  public class TestTimeViewModel : EsuInfoBase
  {
    private readonly List<EsuTimerBase> timers;
    public TestTimeViewModel()
    {
      timers = new List<EsuTimerBase>();
      for (int i = 0; i < 100; i++)
      {
        if (i == 11)
          timers.Add(new EsuAscTimer(TaskCreationOptions.LongRunning) { ID = i.ToString() });
        else
        {
          timers.Add(new EsuAscTimer(TaskCreationOptions.None) { ID = i.ToString() });
        }
      }
      foreach (var esuTimer in timers)
      {
        esuTimer.Start();
      }
    }

    public List<EsuTimerBase> Timers
    {
      get { return timers; }
    }
  }
}
