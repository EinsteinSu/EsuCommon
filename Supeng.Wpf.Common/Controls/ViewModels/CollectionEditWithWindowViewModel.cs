using System.Windows;
using Supeng.Common;
using Supeng.Common.Entities;
using Supeng.Common.Strings;
using Supeng.Wpf.Common.DialogWindows;
using Supeng.Wpf.Common.DialogWindows.ViewModels;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class CollectionEditWithWindowViewModel<TW, T> : CollectionEditViewModel<T>
    where T : EsuInfoBase, new()
    where TW : EsuInfoBase, new()
  {
    private readonly EntityEditViewModelBase<TW> dialogWindowBase;

    protected CollectionEditWithWindowViewModel(EntityEditViewModelBase<TW> dialogWindowBase)
    {
      this.dialogWindowBase = dialogWindowBase;
    }

    public override bool EnableEdit
    {
      get { return false; }
    }

    protected override void InitalizeButton()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) {Name = "Refresh", Description = "刷新数据"});
      ButtonCollection.Add(new EsuButtonBase("增加", 0, Add) {Name = "Add", Description = "新增数据"});
      ButtonCollection.Add(new EsuButtonBase("修改", 0, Modify) {Name = "Modify", Description = "修改数据"});
      ButtonCollection.Add(new EsuButtonBase("删除", 0, Remove) {Name = "Delete", Description = "删除数据"});
      ButtonCollection.Add(new EsuButtonBase("导出", 0, Export) { Name = "Export", Description = "导出至Excel" });
    }

    protected override void Add()
    {
      dialogWindowBase.Data = GetAddItem();
      Window window = dialogWindowBase.ShowDialogWindow();
      if (window.DialogResult != null && window.DialogResult.Value)
      {
        Insert(dialogWindowBase.Data);
        Data.Collection.Add(ReadAfterChanged(dialogWindowBase.Data));
        Data.Collection.AcceptChanges();
      }
    }

    protected virtual void Modify()
    {
      if (Data.CurrentItem != null)
      {
        dialogWindowBase.Data = GetModifyItem();
        Window window = dialogWindowBase.ShowDialogWindow();
        if (window.DialogResult != null && window.DialogResult.Value)
        {
          Update(dialogWindowBase.Data);
          Data.Collection[Data.Collection.IndexOf(Data.CurrentItem)] = ReadAfterChanged(dialogWindowBase.Data);
          Data.Collection.AcceptChanges();
        }
      }
    }

    protected override void Remove()
    {
      if (Data.CurrentItem != null && Utils.DeleteMessage(EntityName))
      {
        Delete(Data.CurrentItem.ToString().Load<TW>());
        Data.Collection.Remove(Data.CurrentItem);
        Data.Collection.AcceptChanges();
      }
    }

    protected virtual TW GetAddItem()
    {
      return InitailizeDefaultData<TW>();
    }

    protected virtual TW GetModifyItem()
    {
      return Data.CurrentItem.ToString().Load<TW>();
    }

    protected abstract void Insert(TW data);

    protected abstract void Update(TW data);

    protected abstract void Delete(TW data);

    protected abstract T ReadAfterChanged(TW data);
  }
}