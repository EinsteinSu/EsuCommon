using System.Windows;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class EntityEditViewModelBase<T> : DialogWindowBase
  {
    private T data;

    public EntityEditViewModelBase() { }

    public EntityEditViewModelBase(T data)
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

    public override FrameworkElement Content
    {
      get { return new EntityEditControl(); }
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}