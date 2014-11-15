using System.Windows;
using Caliburn.Micro;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;
using Supeng.Wpf.Common.DialogWindows.ViewModels;
using Supeng.Wpf.Common.DialogWindows.Views;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows
{
  public static class DialogWindowHelper
  {
    public static Window ShowDialogWindow(this EsuInfoBase viewModel)
    {
      var window = new DialogWindowView();
      ShowDialogWindow(window, viewModel);
      return window;
    }

    public static bool ShowDialogWindowWithReturn(this DialogWindowBase viewModel)
    {
      return new DialogWindowView().ShowDialogWindowWithReturn(viewModel);
    }

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

    public static bool ShowDialogWindowWithReturn(this Window window, EsuInfoBase viewModel)
    {
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
      window.DataContext = viewModel;
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();
      window.ShowDialog();
      if (window.DialogResult != null)
        return window.DialogResult.Value;
      return false;
    }

    public static void ShowReportWindow(ReportWindowBase viewModel)
    {
      var window = new DocumentViewWindow { Title = viewModel.Title, DataContext = viewModel };
      window.ShowWindow(viewModel);
    }

    public static Window ShowWindow(this EsuInfoBase viewModel, System.Action closeWindow = null)
    {
      var window = new DialogWindowView();
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
      window.DataContext = viewModel;
      window.Show();
      window.ContentRendered += (sender, args) =>
      {
        var load = viewModel as IDataLoad;
        if (load != null)
          load.Load();
      };
      if (closeWindow != null)
        window.Closed += (sender, args) => closeWindow();
      return window;
    }

    public static void ShowWindow(this Window window, EsuInfoBase viewModel)
    {
      var windowViewModel = viewModel as IWindowViewModel;
      if (windowViewModel != null)
        windowViewModel.Window = window;
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