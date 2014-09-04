using System.Windows;
using Supeng.Common.Strings;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public class MemoEditWindowViewModel : DialogWindowBase
  {
    private readonly string title;
    private string text;
    public MemoEditWindowViewModel(string title, string defaultText)
    {
      this.title = title;
      text = defaultText;
    }

    public override string Title
    {
      get { return title; }
    }

    protected override string TemplateName
    {
      get { return title.GetPascalName() + "Window"; }
    }

    public string Text
    {
      get { return text; }
      set
      {
        if (value == text) return;
        text = value;
        NotifyOfPropertyChange(() => Text);
      }
    }

    public override sealed FrameworkElement Content
    {
      get { return new MemoTextControl(); }
    }

    protected override string DataCheck()
    {
      if (string.IsNullOrEmpty(text))
        return "内容不能为空！";
      return string.Empty;
    }
  }
}
