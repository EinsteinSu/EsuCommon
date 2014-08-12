using Supeng.Silverlight.Controls.Views;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public abstract class EntityEditViewModelBase<T> : DialogWindowBase
  {
    private T data;

    protected EntityEditViewModelBase(int minWidth = 300)
      : base(new EntityEditControl { MinWidth = minWidth })
    {
    }

    protected EntityEditViewModelBase(T data)
      : this()
    {
      this.data = data;
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