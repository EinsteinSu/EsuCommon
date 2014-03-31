using System.Windows;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.ViewModel.Controls
{
  public class EsuProgressViewModel : EsuInfoBase
  {
    private string message;
    private Visibility progressVisibility = Visibility.Collapsed;

    public string Message
    {
      get { return message; }
      set
      {
        if (value == message) return;
        message = value;
        NotifyOfPropertyChange(() => Message);
      }
    }

    public Visibility ProgressVisibility
    {
      get { return progressVisibility; }
      set
      {
        if (value == progressVisibility) return;
        progressVisibility = value;
        NotifyOfPropertyChange(() => ProgressVisibility);
      }
    }

    public void ShowProgress(string text)
    {
      ShowProgress();
      Message = text;
    }

    public void ShowProgress()
    {
      ProgressVisibility = Visibility.Visible;
    }

    public void HideProgress()
    {
      ProgressVisibility = Visibility.Collapsed;
      Message = string.Empty;
    }
  }
}