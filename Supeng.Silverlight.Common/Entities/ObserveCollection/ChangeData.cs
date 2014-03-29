using System;

namespace Supeng.Silverlight.Common.Entities.ObserveCollection
{
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
}