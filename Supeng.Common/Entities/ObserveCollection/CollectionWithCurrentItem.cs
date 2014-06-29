using System;
using System.Collections.ObjectModel;

namespace Supeng.Common.Entities.ObserveCollection
{
  public class CollectionWithCurrentItem<T> : EsuInfoBase
  {
    private ObservableCollection<T> collection;
    private T currentItem;
    private Action<T> currentItemChangedAction;

    public CollectionWithCurrentItem(Action<T> action = null)
    {
      currentItemChangedAction = action;
      collection = new ObservableCollection<T>();
    }

    public Action<T> CurrentItemChangedAction
    {
      get { return currentItemChangedAction; }
      set
      {
        if (Equals(value, currentItemChangedAction)) return;
        currentItemChangedAction = value;
        NotifyOfPropertyChange(() => CurrentItemChangedAction);
      }
    }

    public ObservableCollection<T> Collection
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
        if (value != null && currentItemChangedAction != null)
          currentItemChangedAction(value);
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }
  }
}