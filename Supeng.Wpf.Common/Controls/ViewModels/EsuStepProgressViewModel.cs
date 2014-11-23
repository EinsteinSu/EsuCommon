using System.Windows;
using Supeng.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public class EsuStepProgressViewModel : EsuInfoBase
  {
    private readonly int maxStep;
    private int step;
    private Visibility visibility;

    public EsuStepProgressViewModel(int maxStep)
    {
      this.maxStep = maxStep;
      NotifyOfPropertyChange(() => MaxStep);
    }

    public Visibility Visibility
    {
      get { return visibility; }
    }

    public int MaxStep
    {
      get { return maxStep; }
    }

    public int Step
    {
      get { return step; }
    }

    public void StepAdd(int s = 10)
    {
      if (Step < MaxStep - 1)
      {
        step += s;
        NotifyOfPropertyChange(() => Step);
      }
      else
      {
        visibility = Visibility.Collapsed;
        NotifyOfPropertyChange(() => Visibility);
      }
    }
  }
}