using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Bars;
using Newtonsoft.Json;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;
using Supeng.Common.IOs;
using Supeng.Common.Strings;
using Supeng.Common.Types;
using Supeng.Wpf.Common.Controls.ViewModels;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  /// <summary>
  ///   implement
  ///   1. add id property
  ///   2. storage the width and height when user change the window resize
  /// </summary>
  public abstract class DialogWindowBase : EsuInfoBase, IWindowViewModel, IDataLoad
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand okCommand;
    private EsuProgressViewModel progress;
    private bool result;
    private Window window;


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

    public virtual void Load()
    {
      if (Content.DataContext == null)
        Content.DataContext = this;
    }

    [JsonIgnore]
    public Window Window
    {
      get { return window; }
      set
      {
        window = value;
        if (window != null)
        {
          window.Title = Title;
          window.Closed += (sender, args) => SaveLayout();
          string templateFileName = string.Format("{0}{1}.txt", DirectoryHelper.TemplateDirectory, TemplateName);
          if (File.Exists(templateFileName))
          {
            string text = File.ReadAllText(templateFileName);
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

    public void SaveLayout()
    {
      string templateFileName = string.Format("{0}{1}.txt", DirectoryHelper.TemplateDirectory, TemplateName);
      string layout = string.Format("{0},{1}", window.Width, window.Height);
      File.WriteAllText(templateFileName, layout);
    }
  }
}