using System.Windows;
using Supeng.Silverlight.Controls.Views;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public class EntityEditViewModelBase<T> : DialogWindowBase
  {
    private T data;

    public EntityEditViewModelBase()
    {
    }

    public EntityEditViewModelBase(T data)
    {
      this.data = data;
    }

    protected override string TemplateName
    {
      get { return "EntityEditViewWindow"; }
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