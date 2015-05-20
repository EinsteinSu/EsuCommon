namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class EntityPreviewWindow<T> : PreviewWindowBase
  {
    private readonly string templateName;

    public EntityPreviewWindow(string templateName)
    {
      this.templateName = templateName;
    }

    protected override string TemplateName
    {
      get { return templateName; }
    }

    private T data;

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