using DevExpress.Xpf.Bars;
using Supeng.Common.Interfaces;

namespace Supeng.Wpf.Common.Commands
{
  public class CrudCommands
  {
    private readonly DelegateCommand addCommand;
    private readonly DelegateCommand modifyCommand;
    private readonly DelegateCommand deleteCommand;

    public CrudCommands(ICrud crud)
    {
      addCommand = new DelegateCommand(crud.Add, () => true);
      modifyCommand = new DelegateCommand(crud.Modify, () => true);
      deleteCommand = new DelegateCommand(crud.Delete, () => true);
    }

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
  }
}
