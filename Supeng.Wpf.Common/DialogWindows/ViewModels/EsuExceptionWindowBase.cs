using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Bars;
using Supeng.Common.IOs;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class EsuExceptionWindowBase : DialogWindowBase
  {
    private readonly string message;
    private readonly string stackTrace;
    private readonly DelegateCommand closeCommand;
    private readonly DelegateCommand sendCommand;
    private readonly DelegateCommand emailCommand;

    protected EsuExceptionWindowBase(Exception ex)
    {
      message = ex.Message;
      stackTrace = ex.StackTrace;
      closeCommand = new DelegateCommand(Close, () => true);
      sendCommand = new DelegateCommand(Send, () => true);
      emailCommand = new DelegateCommand(Email, () => true);
    }

    public override string Title
    {
      get { return "系统异常"; }
    }

    public virtual ImageSource Image
    {
      get
      {
        var fileName = string.Format("{0}exception.png", DirectoryHelper.ImageDirectory);
        if (File.Exists(fileName))
          return new BitmapImage(new Uri(fileName, UriKind.Absolute));
        return null;
      }
    }

    #region commands and operations
    public DelegateCommand CloseCommand
    {
      get { return closeCommand; }
    }

    public DelegateCommand SendCommand
    {
      get { return sendCommand; }
    }

    public DelegateCommand EmailCommand
    {
      get { return emailCommand; }
    }

    protected abstract void Send();

    protected abstract void Email();

    protected virtual void Close()
    {
      if (Window != null)
        Window.Close();
    }

    #endregion

    protected override string DataCheck()
    {
      return string.Empty;
    }

    public string Message
    {
      get { return message; }
    }

    public string StackTrace
    {
      get { return stackTrace; }
    }

    public override FrameworkElement Content
    {
      get { return new ExceptionControl(); }
    }
  }
}
