using System.Collections.Generic;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using Supeng.Common.Entities;
using Supeng.Sports.Common.Timers;

namespace Supeng.Sports.Common.Tests
{
  /// <summary>
  ///   Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow
  {
    private readonly TestTimeViewModel viewModel;

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
        timers.Add(i == 11
          ? new EsuAscTimer(TaskCreationOptions.LongRunning) {ID = i.ToString(CultureInfo.InvariantCulture)}
          : new EsuAscTimer(TaskCreationOptions.None) {ID = i.ToString(CultureInfo.InvariantCulture)});
      }
      foreach (EsuTimerBase esuTimer in timers)
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