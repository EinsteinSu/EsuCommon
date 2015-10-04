using Supeng.Common.Entities;

namespace Supeng.Wpf.Common.DialogWindows.Models
{
  public class SelectionModel<T> : EsuInfoBase where T : EsuInfoBase
  {
    private T data;
    private bool selected;

    public SelectionModel()
    {
    }

    public SelectionModel(T data)
    {
      Selected = false;
      Data = data;
    }

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