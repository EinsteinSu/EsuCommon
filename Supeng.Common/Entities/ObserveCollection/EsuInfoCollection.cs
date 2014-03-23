using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Newtonsoft.Json;

namespace Supeng.Common.Entities.ObserveCollection
{
  public class EsuInfoCollection<T> : ObservableCollection<T> where T : EsuInfoBase, new()
  {
    private ChangesCollection<T> changedCollection;

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

    protected override void InsertItem(int index, T item)
    {
      base.InsertItem(index, item);
      changedCollection.Add(new ChangeData<T> { Data = item, ChangeTime = DateTime.Now, State = EsuDataState.Added });
      var notifyPropertyChanged = item as INotifyPropertyChanged;
      if (notifyPropertyChanged != null)
        notifyPropertyChanged.PropertyChanged += DataChanged;
    }

    public virtual void DataChanged(object sender, PropertyChangedEventArgs e)
    {
      var data = (T)sender;
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

  public class ChangesCollection<T> : ObservableCollection<ChangeData<T>> where T : EsuInfoBase
  {
    public Action DataAdded { get; set; }

    protected override void InsertItem(int index, ChangeData<T> item)
    {
      base.InsertItem(index, item);
      if (DataAdded != null)
        DataAdded();
    }
  }

  public class ChangeData<T> : EsuInfoBase where T : EsuInfoBase
  {
    private EsuDataState state;
    private T data;
    private DateTime changeTime;

    #region properties
    public EsuDataState State
    {
      get { return state; }
      set
      {
        if (value == state) return;
        state = value;
        NotifyOfPropertyChange(() => State);
      }
    }

    public T Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    public DateTime ChangeTime
    {
      get { return changeTime; }
      set
      {
        if (value.Equals(changeTime)) return;
        changeTime = value;
        NotifyOfPropertyChange(() => ChangeTime);
      }
    }
    #endregion

    public override string ToString()
    {
      if (!string.IsNullOrEmpty(Description))
        return string.Format("{0} is {1}, changed on {2}({3}).", Data, State, ChangeTime, Description);
      return string.Format("{0} is {1}, changed on {2}.", Data, State, ChangeTime);
    }
  }

  public enum EsuDataState
  {
    Added,
    Modified,
    Deleted
  }
}
