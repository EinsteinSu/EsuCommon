using Supeng.Silverlight.Controls.Views;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public class EntityEditViewModelBase<T> : DialogWindowBase
  {
    private T data;

    public EntityEditViewModelBase()
      : base(new EntityEditControl())
    {
    }

    public EntityEditViewModelBase(T data)
      : this()
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

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}