using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;

namespace Supeng.Data
{
  public abstract class EsuDataEditCollection<T> : EsuInfoBase, IDataLoad where T : EsuInfoBase, new()
  {
    protected readonly DataStorageBase Storage;
    private EsuInfoCollection<T> collection;
    private T currentItem;

    protected EsuDataEditCollection(DataStorageBase storage)
    {
      Storage = storage;
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
        CurrentItemChanged(value);
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }

    public void Load()
    {
      LoadCollection(Storage);
    }

    protected abstract void LoadCollection(DataStorageBase storage);

    public abstract void Save();

    protected abstract void CurrentItemChanged(T current);
  }
}