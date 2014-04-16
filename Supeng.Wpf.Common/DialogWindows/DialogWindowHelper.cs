using Supeng.Wpf.Common.DialogWindows.ViewModels;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows
{
  public static class DialogWindowHelper
  {
    public static T GetEntityResult<T>(EntityEditViewModelBase<T> viewModel)
    {
      var window = new EntityEditView();
      viewModel.Window = window;
      window.DataContext = viewModel;
      window.ShowDialog();
      if (window.DialogResult.HasValue && window.DialogResult.Value)
        return viewModel.Data;
      return default(T);
    }
  }
}