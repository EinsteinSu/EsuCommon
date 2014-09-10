using System.Collections.Generic;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities.BasesEntities;

namespace Supeng.Wpf.Common.Commands
{
  public class MultiplySelectionCommand<T>
  {
    private readonly IList<MutiplySelectionEntityBase<T>> data;
    private readonly DelegateCommand revertCommand;
    private readonly DelegateCommand selectAllCommand;
    private readonly DelegateCommand unSelectCommand;

    public MultiplySelectionCommand(IList<MutiplySelectionEntityBase<T>> data)
    {
      this.data = data;
      selectAllCommand = new DelegateCommand(SelectAll, () => true);
      revertCommand = new DelegateCommand(Revert, () => true);
      unSelectCommand = new DelegateCommand(UnSelect, () => true);
    }

    public DelegateCommand SelectAllCommand
    {
      get { return selectAllCommand; }
    }

    public DelegateCommand RevertCommand
    {
      get { return revertCommand; }
    }

    public DelegateCommand UnSelectCommand
    {
      get { return unSelectCommand; }
    }

    protected virtual void SelectAll()
    {
      foreach (var item in data)
      {
        item.Selected = true;
      }
    }

    protected virtual void Revert()
    {
      foreach (var item in data)
      {
        item.Selected = !item.Selected;
      }
    }

    protected virtual void UnSelect()
    {
      foreach (var item in data)
      {
        item.Selected = false;
      }
    }
  }
}