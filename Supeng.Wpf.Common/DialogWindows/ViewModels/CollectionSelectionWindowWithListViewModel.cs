using System.Windows;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class CollectionSelectionWindowWithListViewModel<T> : CollectionSelectionWindowViewModelBase<T> where T : EsuInfoBase, new()
  {
    public override FrameworkElement Content
    {
      get { return new ListSelectionControl(); }
    }

    public string DisplayMember
    {
      get { return "Name"; }
    }
  }
}
