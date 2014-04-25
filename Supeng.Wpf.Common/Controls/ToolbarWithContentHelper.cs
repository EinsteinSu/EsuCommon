using System.Windows;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;
using Supeng.Common.Threads;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.Controls.Views;

namespace Supeng.Wpf.Common.Controls
{
  public static class ToolbarWithContentHelper
  {
    public static FrameworkElement GetToolbarWithContentControl(ToolbarWithContentViewModelBase viewModel)
    {
      var view = new ToolbarWithContentView { DataContext = viewModel };
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();
      return view;
    }

    public static FrameworkElement GetCollectionEditControl<T>(CollectionEditViewModel<T> viewModel, IBackgroundData<EsuInfoCollection<T>> background = null) where T : EsuInfoBase, new()
    {
      var view = new ToolbarWithContentView { DataContext = viewModel };
      var dataLoad = viewModel as IDataLoad;
      if (dataLoad != null)
      {
        dataLoad.Load();
        if (viewModel.Data != null)
          viewModel.Data.BackgroundHandler = background;
      }
      return view;
    }
  }
}