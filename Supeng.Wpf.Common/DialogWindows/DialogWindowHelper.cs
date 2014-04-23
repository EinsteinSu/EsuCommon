using System.Windows;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;
using Supeng.Wpf.Common.DialogWindows.ViewModels;
using Supeng.Wpf.Common.DialogWindows.Views;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows
{
  public static class DialogWindowHelper
  {
    public static void ShowDialogWindow(this Window window, EsuInfoBase viewModel)
    {
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
      window.DataContext = viewModel;
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();
      window.ShowDialog();
    }

    public static void ShowWindow(this  Window window, EsuInfoBase viewModel)
    {
      window.DataContext = viewModel;
      window.Show();
      window.ContentRendered += (sender, args) =>
      {
        var load = viewModel as IDataLoad;
        if (load != null)
          load.Load();
      };
    }

    public static T GetEntityResult<T>(EntityEditViewModelBase<T> viewModel)
    {
      var window = new DialogWindowView();
      window.ShowDialogWindow(viewModel);
      if (window.DialogResult.HasValue && window.DialogResult.Value)
        return viewModel.Data;
      return default(T);
    }
  }
}