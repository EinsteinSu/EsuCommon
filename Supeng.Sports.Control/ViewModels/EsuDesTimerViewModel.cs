using System.Threading.Tasks;
using Supeng.Sports.Common.Timers;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Sports.Control.ViewModels
{
  public class EsuDesTimerViewModel : EsuDescTimer
  {
    private readonly EsuToolBarButtonCollection buttonCollection;
    private readonly EsuButtonBase resetButton;
    private readonly EsuButtonBase startButton;
    private readonly EsuButtonBase stopButton;

    public EsuDesTimerViewModel()
      : base(0, 0, TaskCreationOptions.LongRunning)
    {
      buttonCollection = new EsuToolBarButtonCollection(EsuButtonToolBarType.TextAndImage);
      startButton = new EsuButtonBase(Start) { Text = "Start", Description = "Start time" };
      stopButton = new EsuButtonBase(Stop) { Text = "Stop", Description = "Stop time" };
      resetButton = new EsuButtonBase(Reset) { Text = "Reset", Description = "Reset time" };
      buttonCollection.Add(startButton);
      buttonCollection.Add(stopButton);
      buttonCollection.Add(resetButton);
      NotifyOfPropertyChange(() => ButtonCollection);
      StopButton.Enable = false;
    }

    #region buttons
    public EsuToolBarButtonCollection ButtonCollection
    {
      get { return buttonCollection; }
    }

    public EsuButtonBase StartButton
    {
      get { return startButton; }
    }

    public EsuButtonBase StopButton
    {
      get { return stopButton; }
    }

    public EsuButtonBase ResetButton
    {
      get { return resetButton; }
    }
    #endregion

    protected override void BeforeStart()
    {
      StartButton.Enable = false;
      StopButton.Enable = true;
    }

    protected override void Stoped()
    {
      StartButton.Enable = true;
      StopButton.Enable = false;
    }

    protected override void Reseted()
    {
      StartButton.Enable = true;
      StopButton.Enable = false;
    }
  }
}