using System;

namespace Supeng.Common.Entities.ObserveCollection
{
  public class EsuEditCollection<T> : EsuInfoBase where T : EsuInfoBase
  {
    private EsuInfoCollection<T> collection;
    private T currentItem;
    private Action<T> currentItemChangedAction;

    public EsuEditCollection(Action<T> action = null)
    {
      collection = new EsuInfoCollection<T>();
      currentItemChangedAction = action;
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
        if (value != null && currentItemChangedAction != null)
          currentItemChangedAction(value);
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }
  }
}