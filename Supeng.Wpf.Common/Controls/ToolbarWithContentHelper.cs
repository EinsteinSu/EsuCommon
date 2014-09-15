using System.Windows;
using Supeng.Common.Controls;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;
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

    public static FrameworkElement GetCollectionEditControl<T>(CollectionEditViewModel<T> viewModel)
      where T : EsuInfoBase, new()
    {
      return GetToolbarWithContentControl(viewModel);
    }
  }
}