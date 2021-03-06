﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Supeng.Silverlight.Common.Entities.ObserveCollection
{
  public class EsuInfoCollection<T> : ObservableCollection<T> where T : EsuInfoBase, new()
  {
    private ChangesCollection<T> changedCollection;
    private T currentItem;

    public EsuInfoCollection()
    {
      changedCollection = new ChangesCollection<T>();
    }

    public bool HasChanged
    {
      get { return changedCollection.Any(); }
    }

    public ChangesCollection<T> ChangedCollection
    {
      get { return changedCollection; }
    }

    public Action<T> CurrentItemChanged { get; set; }

    public T CurrentItem
    {
      get { return currentItem; }
      set
      {
        currentItem = value;
        if (CurrentItemChanged != null)
          CurrentItemChanged(value);
        OnPropertyChanged(new PropertyChangedEventArgs("CurrentItem"));
      }
    }

    public Action<EsuDataState, T> EsuCollectionChanged { get; set; }

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      if (EsuCollectionChanged != null)
        EsuCollectionChanged(EsuDataState.Added, item);
      changedCollection.Add(new ChangeData<T> {Data = item, ChangeTime = DateTime.Now, State = EsuDataState.Added});
      var notifyPropertyChanged = item as INotifyPropertyChanged;
      if (notifyPropertyChanged != null)
        notifyPropertyChanged.PropertyChanged += DataChanged;
    }

    public virtual void DataChanged(object sender, PropertyChangedEventArgs e)
    {
      var data = (T) sender;
      if (EsuCollectionChanged != null)
        EsuCollectionChanged(EsuDataState.Modified, data);
      if (changedCollection.Any(w => data.Equals(w.Data)))
        return;
      changedCollection.Add(new ChangeData<T>
      {
        Data = data,
        ChangeTime = DateTime.Now,
        State = EsuDataState.Modified,
        Description = e.PropertyName
      });
    }

    protected override void RemoveItem(int index)
    {
      T data = Items[index];
      if (EsuCollectionChanged != null)
        EsuCollectionChanged(EsuDataState.Deleted, data);
      while (true)
      {
        if (!changedCollection.Any(w => data.Equals(w.Data)))
          break;
        changedCollection.Remove(changedCollection.First(f => data.Equals(f.Data)));
      }
      changedCollection.Add(new ChangeData<T>
      {
        Data = data,
        ChangeTime = DateTime.Now,
        State = EsuDataState.Deleted
      });
      var notifyPropertyChanged = Items[index] as INotifyPropertyChanged;
      if (notifyPropertyChanged != null)
        notifyPropertyChanged.PropertyChanged -= DataChanged;
      base.RemoveItem(index);
    }

    public void RemoveAll()
    {
      while (true)
      {
        if (this.Any())
          RemoveItem(0);
        else
        {
          break;
        }
      }
    }

    public void AcceptChanges()
    {
      changedCollection = new ChangesCollection<T>();
    }

    public override string ToString()
    {
      return JsonConvert.SerializeObject(this);
    }
  }
}