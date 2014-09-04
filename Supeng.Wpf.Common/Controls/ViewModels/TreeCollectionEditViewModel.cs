using System;
using System.Linq;
using System.Windows;
using Supeng.Common;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;
using Supeng.Wpf.Common.Controls.Views;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class TreeCollectionEditViewModel<T> : ToolbarWithContentViewModelBase, IDataLoad
    where T : TreeEntityBase, new()
  {
    private EsuInfoCollection<T> data;

    protected TreeCollectionEditViewModel()
    {
      InitalizeButton();
    }

    public virtual string KeyID
    {
      get { return "ID"; }
    }

    public virtual string ParentID
    {
      get { return "PID"; }
    }

    public override FrameworkElement Content
    {
      get { return new CollectionEditTree(); }
    }

    public EsuInfoCollection<T> Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    protected override void InitalizeButton()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) { Name = "Refresh", Description = "刷新" });
      ButtonCollection.Add(new EsuButtonBase("增加", 0, Add) { Name = "Add", Description = "新增" });
      ButtonCollection.Add(new EsuButtonBase("增加子项", 0, AddChild) { Name = "AddChild", Description = "增加当前节点的子节点" });
      ButtonCollection.Add(new EsuButtonBase("删除", 0, Remove) { Name = "Delete", Description = "删除" });
      ButtonCollection.Add(new EsuButtonBase("保存", 0, Save) { Name = "Save", Description = "保存数据" });
    }

    protected virtual void Add()
    {
      var newData = InitailizeData();
      newData.PID = "0";
      Data.Add(newData);
    }

    protected virtual void AddChild()
    {
      if (data.CurrentItem == null)
        return;
      var child = InitailizeData();
      child.PID = data.CurrentItem.ID;
      data.Add(child);
      Data.CurrentItem = data[data.IndexOf(child)];
    }

    protected virtual void Remove()
    {
      if (data.CurrentItem != null && Utils.DeleteMessage(EntityName))
        DeleteItem(data.CurrentItem.ID);
    }

    protected virtual void DeleteItem(string id)
    {
      var deleteData = data.FirstOrDefault(f => f.ID.Equals(id, StringComparison.InvariantCultureIgnoreCase));
      if (deleteData != null)
        Data.Remove(deleteData);
      var first = data.FirstOrDefault(f => f.PID.Equals(id, StringComparison.InvariantCultureIgnoreCase));
      if (first != null)
        DeleteItem(first.ID);
    }

    protected abstract string EntityName { get; }

    protected abstract EsuInfoCollection<T> LoadData();

    public abstract void Save();

    protected virtual T InitailizeData()
    {
      return InitailizeDefaultData<T>();
    }

    public virtual void Load()
    {
      Data = LoadData();
    }
  }
}
