using System.Windows;
using DevExpress.Xpf.Grid;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class TreeCollectionSelectionWindowViewModel<T> : CollectionSelectionWindowViewModel<T>
    where T : EsuInfoBase, new()
  {
    public virtual string KeyName
    {
      get { return "ID"; }
    }

    public virtual string ParentKeyName
    {
      get { return "PID"; }
    }

    public override FrameworkElement Content
    {
      get { return new TreeSelectionControl(); }
    }
  }
}