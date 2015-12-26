using System.Windows;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Interfaces.Controls;

namespace Supeng.Silverlight.Controls.ViewModels
{
  public class EsuProgressViewModel : EsuInfoBase, IProgress
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
        EsuNotifyOfPropertyChange(() => Message);
      }
    }

    public Visibility ProgressVisibility
    {
      get { return progressVisibility; }
      set
      {
        if (value == progressVisibility) return;
        progressVisibility = value;
        EsuNotifyOfPropertyChange(() => ProgressVisibility);
      }
    }

    public void ShowProgress(string text)
    {
      ShowProgress();
      Message = text;
    }

    public void HideProgress()
    {
      ProgressVisibility = Visibility.Collapsed;
      Message = string.Empty;
    }

    public void ShowProgress()
    {
      ProgressVisibility = Visibility.Visible;
    }
  }
}