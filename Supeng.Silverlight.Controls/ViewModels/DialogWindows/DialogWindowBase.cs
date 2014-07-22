using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.IOs;
using Supeng.Silverlight.Common.Strings;
using Supeng.Silverlight.Common.Types;
using Supeng.Silverlight.Controls.Interfaces;

namespace Supeng.Silverlight.Controls.ViewModels.DialogWindows
{
  public abstract class DialogWindowBase : EsuInfoBase, IWindowViewModel, IDataLoad
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand okCommand;
    private EsuProgressViewModel progress;
    private bool result;
    private ChildWindow window;


    protected DialogWindowBase()
    {
      okCommand = new DelegateCommand(OkClick, () => true);
      cancelCommand = new DelegateCommand(CancelClick, () => true);
      progress = new EsuProgressViewModel();
    }

    protected virtual string TemplateName
    {
      get { return "DialogWindowBase"; }
    }

    [JsonIgnore]
    public abstract FrameworkElement Content { get; }

    #region IWindowViewModel implement

    public virtual string Title
    {
      get { return string.Empty; }
    }

    public int Height
    {
      get { return 400; }
    }

    public int Width
    {
      get { return 400; }
    }

    #endregion

    #region properties

    [JsonIgnore]
    public EsuProgressViewModel Progress
    {
      get { return progress; }
      set
      {
        if (Equals(value, progress)) return;
        progress = value;
        NotifyOfPropertyChange(() => Progress);
      }
    }

    [JsonIgnore]
    public bool Result
    {
      get { return result; }
    }

    [JsonIgnore]
    public DelegateCommand OkCommand
    {
      get { return okCommand; }
    }

    [JsonIgnore]
    public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    #endregion

    public virtual void Load()
    {
      if (Content.DataContext == null)
        Content.DataContext = this;
    }

    [JsonIgnore]
    public ChildWindow Window
    {
      get { return window; }
      set
      {
        window = value;
        if (window != null)
        {
          window.Title = Title;
          window.Closed += (sender, args) => SaveLayout();
          string templateFileName = string.Format("{0}.txt", TemplateName);
          string text = StreamHelper.ReadText(templateFileName);
          if (!string.IsNullOrEmpty(text))
          {
            List<string> list = text.GetStringCollection(',');
            if (list.Any())
            {
              window.Width = list[0].ConvertData(0);
              window.Height = list[1].ConvertData(0);
            }
          }
          else
          {
            window.Height = Height;
            window.Width = Width;
          }
        }
      }
    }

    private void SaveLayout()
    {
      string templateFileName = string.Format("{0}.txt", TemplateName);
      string layout = string.Format("{0},{1}", window.Width, window.Height);
      StreamHelper.WriteText(templateFileName, layout);
    }

    protected abstract string DataCheck();

    protected virtual void OkClick()
    {
      string errMsg = DataCheck();
      if (!string.IsNullOrEmpty(errMsg))
      {
        MessageBox.Show(errMsg);
        return;
      }
      result = true;
      window.DialogResult = true;
    }

    public virtual void CancelClick()
    {
      result = false;
      window.DialogResult = false;
    }
  }
}