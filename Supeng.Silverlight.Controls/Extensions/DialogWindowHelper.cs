using System;
using DevExpress.Xpf.Core;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Controls.Interfaces;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;
using Supeng.Silverlight.Controls.Views;

namespace Supeng.Silverlight.Controls.Extensions
{
  public static class DialogWindowHelper
  {
    public static void ShowDialogWindow(this DXWindow window, DialogWindowBase viewModel, Action<bool> showDone)
    {
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
      window.DataContext = viewModel;
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();
      window.ShowDialog();
      window.Closed += (sender, args) =>
      {
        if (showDone != null)
          showDone(viewModel.Result);
      };
    }

    public static void ShowDialogWindow(DialogWindowBase viewModel, Action<bool> showDone)
    {
      new DialogWindowView().ShowDialogWindow(viewModel, showDone);
    }
  }
}