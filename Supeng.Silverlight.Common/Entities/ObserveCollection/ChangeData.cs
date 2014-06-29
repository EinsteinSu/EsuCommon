using System;
using System.Collections.Generic;

namespace Supeng.Silverlight.Common.Entities.ObserveCollection
{
  public class ChangeData<T> : EsuInfoBase where T : EsuInfoBase
  {
    private DateTime changeTime;
    private T data;
    private Dictionary<string, object> extensions = new Dictionary<string, object>();
    private EsuDataState state;

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

    public Dictionary<string, object> Extensions
    {
      get { return extensions; }
      set
      {
        if (Equals(value, extensions)) return;
        extensions = value;
        NotifyOfPropertyChange(() => Extensions);
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
}