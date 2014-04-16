using System;
using System.Collections;
using System.Collections.Generic;

namespace Supeng.Common.Entities.ObserveCollection
{
  public class ChangeData<T> : EsuInfoBase where T : EsuInfoBase
  {
    private EsuDataState state;
    private T data;
    private DateTime changeTime;
    private IDictionary<string, string> extensions = new Dictionary<string, string>();

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

    public IDictionary<string, string> Extensions
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
        return string.Format("{0} is {1}, changed on {2}({3}).", Data.ID, State, ChangeTime, Description);
      return string.Format("{0} is {1}, changed on {2}.", Data.ID, State, ChangeTime);
    }
  }
}