using System;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Controls.ViewModels.DialogWindows;

namespace Supeng.Silverlight.Controls.ViewModels
{
  public class MemoTextWindowViewModel : DialogWindowBase
  {
    private string text;
    private bool enabled;
    private readonly DelegateCommand copyCommand;
    private readonly DelegateCommand pasterCommand;

    public MemoTextWindowViewModel(FrameworkElement content, string text, bool enabled = false)
      : base(content)
    {
      this.text = text;
      this.enabled = enabled;
      copyCommand = new DelegateCommand(Copy, () => true);
      pasterCommand = new DelegateCommand(Paster, () => true);
      NotifyOfPropertyChange(() => Text);
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
