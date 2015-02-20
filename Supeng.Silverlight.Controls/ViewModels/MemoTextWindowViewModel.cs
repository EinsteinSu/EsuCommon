using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;

namespace Supeng.Silverlight.Controls.ViewModels
{
  public class MemoTextWindowViewModel : DialogWindowBase
  {
    private string text;
    private readonly string templateName;
    private bool enabled;
    private readonly string title;
    private readonly int height;
    private readonly int width;
    private readonly DelegateCommand copyCommand;
    private readonly DelegateCommand pasterCommand;

    public MemoTextWindowViewModel(FrameworkElement content, string text, string templateName, bool enabled = false)
      : base(content)
    {
      this.text = text;
      this.templateName = templateName;
      this.enabled = enabled;
      copyCommand = new DelegateCommand(Copy, () => true);
      pasterCommand = new DelegateCommand(Paster, () => true);
      NotifyOfPropertyChange(() => Text);
    }

    public MemoTextWindowViewModel(FrameworkElement content, string text, string templateName, string title,
      int height = 400, int width = 300, bool enabled = false)
      : this(content, text, templateName, enabled)
    {
      this.title = title;
      this.height = height;
      this.width = width;
    }

    public override int Height
    {
      get { return height; }
    }

    public override int Width
    {
      get { return width; }
    }

    public override string Title
    {
      get { return title; }
    }

    protected override string TemplateName
    {
      get { return templateName; }
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

    public bool Enabled
    {
      get { return enabled; }
      set
      {
        if (value.Equals(enabled)) return;
        enabled = value;
        NotifyOfPropertyChange(() => Enabled);
      }
    }

    #region commands
    public DelegateCommand CopyCommand
    {
      get { return copyCommand; }
    }

    public DelegateCommand PasterCommand
    {
      get { return pasterCommand; }
    }

    protected virtual void Copy()
    {
      try
      {
        Clipboard.SetText(text ?? string.Empty);
      }
      catch
      {
        MessageBox.Show("用户取消了该操作！");
      }
    }

    protected virtual void Paster()
    {
      try
      {
        if (text == null)
          text = string.Empty;
        var data = Clipboard.GetText() ?? string.Empty;
        Text += data;
      }
      catch
      {
        MessageBox.Show("用户取消了该操作！");
      }

    }
    #endregion

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}
