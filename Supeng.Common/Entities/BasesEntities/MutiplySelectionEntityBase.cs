using Caliburn.Micro;

namespace Supeng.Common.Entities.BasesEntities
{
  public class MutiplySelectionEntityBase<T> : PropertyChangedBase
  {
    private bool selected;
    private T data;

    public bool Selected
    {
      get { return selected; }
      set
      {
        if (value.Equals(selected)) return;
        selected = value;
        NotifyOfPropertyChange(() => Selected);
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
  }
}
