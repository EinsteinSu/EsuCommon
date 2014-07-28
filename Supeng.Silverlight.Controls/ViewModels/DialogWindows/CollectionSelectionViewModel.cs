using System.Windows;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public class CollectionSelectionViewModel<T> : DialogWindowBase where T : EsuInfoBase, new()
  {
    private EsuInfoCollection<T> collection;

    public CollectionSelectionViewModel(FrameworkElement content)
      : base(content)
    {
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

    protected override string DataCheck()
    {
      if (collection.CurrentItem == null)
        return "请选择一项数据";
      return string.Empty;
    }
  }
}
