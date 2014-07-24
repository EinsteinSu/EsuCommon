using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities;

namespace Supeng.Silverlight.Controls.ViewModels
{
  public abstract class CrudCommandBase<T> : EsuInfoBase
  {
    private T currentItem;
    private readonly DelegateCommand addCommand;
    private readonly DelegateCommand modifyCommand;
    private readonly DelegateCommand deleteCommand;

    protected CrudCommandBase()
    {
      addCommand = new DelegateCommand(Add, () => true);
      modifyCommand = new DelegateCommand(Modify, () => true);
      deleteCommand = new DelegateCommand(Delete, () => true);
    }

    #region commands
    public DelegateCommand AddCommand
    {
      get { return addCommand; }
    }

    public DelegateCommand ModifyCommand
    {
      get { return modifyCommand; }
    }

    public DelegateCommand DeleteCommand
    {
      get { return deleteCommand; }
    }
    #endregion

    public T CurrentItem
    {
      get { return currentItem; }
      set
      {
        if (Equals(value, currentItem)) return;
        currentItem = value;
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }

    protected abstract void Add();

    protected abstract void Modify();

    protected abstract void Delete();
  }
}
