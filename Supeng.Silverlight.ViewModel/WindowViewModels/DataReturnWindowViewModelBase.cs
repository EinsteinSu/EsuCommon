using System.Windows.Controls;
using System.Windows.Media;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.ViewModel.WindowViewModels
{
  public abstract class DataReturnWindowViewModelBase : EsuInfoBase
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand okCommand;
    private bool result;

    protected DataReturnWindowViewModelBase()
    {
      okCommand = new DelegateCommand(Ok, () => true);
      cancelCommand = new DelegateCommand(Cancel, () => true);
    }

    public virtual ImageSource Image
    {
      get { return null; }
    }

    public ChildWindow DialogWindow { get; set; }

    public virtual string OkButtonContent
    {
      get { return "确定"; }
    }

    public virtual string CancelButtonContent
    {
      get { return "取消"; }
    }


    public bool Result
    {
      get { return result; }
    }

    #region command

    public DelegateCommand OkCommand
    {
      get { return okCommand; }
    }

    public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    #endregion

    protected virtual void Ok()
    {
      if (DataCheck())
      {
        result = true;
        if (DialogWindow != null)
          DialogWindow.DialogResult = true;
      }
    }

    protected virtual void Cancel()
    {
      result = false;
      if (DialogWindow != null)
        DialogWindow.DialogResult = false;
    }

    public abstract bool DataCheck();
  }
}