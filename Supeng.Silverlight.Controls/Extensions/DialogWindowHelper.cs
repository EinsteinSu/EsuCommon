using System;
using System.Windows.Controls;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Controls.Interfaces;

namespace Supeng.Silverlight.Controls.Extensions
{
  public static class DialogWindowHelper
  {
    public static void ShowDialogWindow(this ChildWindow window, EsuInfoBase viewModel, Action<bool> showDone)
    {
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
      window.DataContext = viewModel;
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();
      window.Show();
      window.Closed += (sender, args) =>
      {
        if (window.DialogResult != null && showDone != null)
        {
          showDone(window.DialogResult.Value);
        }
      };
    }
  }
}