using System.Windows;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public class TreeSelectionWindowViewModel<T> : CollectionSelectionViewModel<T> where T : EsuInfoBase, new()
  {
    public TreeSelectionWindowViewModel(FrameworkElement content)
      : base(content)
    {
    }

    public virtual string KeyName
    {
      get { return "ID"; }
    }

    public virtual string ParentKeyName
    {
      get { return "PID"; }
    }
  }
}
