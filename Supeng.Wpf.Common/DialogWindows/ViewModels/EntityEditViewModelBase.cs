using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class EntityEditViewModelBase<T> : EsuInfoBase, IWindowViewModel
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand okCommand;
    private T data;

    protected EntityEditViewModelBase(T data)
    {
      this.data = data;
      okCommand = new DelegateCommand(Ok, () => true);
      cancelCommand = new DelegateCommand(Cancel, () => true);
    }

    #region IWindowViewModel implement

    public virtual string Title
    {
      get { return string.Empty; }
    }

    public virtual int Height
    {
      get { return 400; }
    }

    public virtual int Width
    {
      get { return 400; }
    }

    #endregion

    public Window Window { get; set; }

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

    public abstract string DataCheck();

    protected virtual void Ok()
    {
      string errMsg = DataCheck();
      if (!string.IsNullOrEmpty(errMsg))
      {
        MessageBox.Show(errMsg);
        return;
      }
      Window.DialogResult = true;
    }

    protected virtual void Cancel()
    {
      Window.DialogResult = false;
    }

    #region commands

    public DelegateCommand OkCommand
    {
      get { return okCommand; }
    }

    public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    #endregion
  }
}