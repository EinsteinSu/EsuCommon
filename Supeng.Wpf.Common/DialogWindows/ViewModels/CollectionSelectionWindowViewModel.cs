using System.Windows;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class CollectionSelectionWindowViewModel<T> : DialogWindowBase where T : EsuInfoBase, new()
  {
    private EsuInfoCollection<T> collection;
    private T currentItem;
    private EsuProgressViewModel progress;

    public EsuProgressViewModel Progress
    {
      get { return progress; }
      set
      {
        if (Equals(value, progress)) return;
        progress = value;
        NotifyOfPropertyChange(() => Progress);
      }
    }

    public override FrameworkElement Content
    {
      get { return new GridSelectionControl(); }
    }

    public EsuInfoCollection<T> Collection
    {
      get { return collection; }
      set
      {
        if (Equals(value, collection)) return;
        collection = value;
        NotifyOfPropertyChange(() => Collection);
      }
    }

    public T CurrentItem
    {
      get { return currentItem; }
      set
      {
        if (Equals(value, currentItem)) return;
        currentItem = value;
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }

    protected override string DataCheck()
    {
      if (currentItem == null)
        return "请选择一项数据";
      return string.Empty;
    }
  }
}