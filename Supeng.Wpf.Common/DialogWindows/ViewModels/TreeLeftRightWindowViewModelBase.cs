using System.Windows;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class TreeLeftRightWindowViewModelBase<T> : LeftRightWindowViewModelBase<T> where T : TreeEntityBase
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
      get { return new LeftRightTreeControl(); }
    }

    protected override EsuInfoCollection<T> InitalizeLeftCollection()
    {
      return new EsuInfoCollection<T>();
    }

    protected override EsuInfoCollection<T> InitalizeRightCollection()
    {
      return new EsuInfoCollection<T>();
    }
  }
}