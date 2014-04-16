using System.Windows.Controls;
using Supeng.Silverlight.Common.Entities.BasesEntities;
using Supeng.Silverlight.ViewModel.WindowViewModels;
using Supeng.Silverlight.Views.WindowControls;

namespace Supeng.Silverlight.Views
{
  public static class ViewsHelper
  {
    public static ChildWindow ShowTreeSelectionWindow<T>(TreeSelectionViewModel<T> data) where T : TreeEntityBase, new()
    {
      var window = new TreeSelectionView();
      data.Window = window;
      window.DataContext = data;
      data.Load();
      window.Show();
      return window;
    }
  }
}