using System.Windows;
using Supeng.Common;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;
using Supeng.Data;
using Supeng.Wpf.Common.Controls.Views;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class CollectionEditViewModel<T> : ToolbarWithContentViewModelBase, IDataLoad
    where T : EsuInfoBase, new()
  {
    private EsuDataEditCollection<T> data;

    protected CollectionEditViewModel()
    {
      InitalizeButton();
    }

    public override FrameworkElement Content
    {
      get { return new CollectionEditGrid(); }
    }

    public EsuDataEditCollection<T> Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    public virtual bool EnableEdit
    {
      get { return true; }
    }

    public abstract string EntityName { get; }

    public void Load()
    {
      data = GetData();
      if (data != null)
      {
        data.Load();
        NotifyOfPropertyChange(() => Data);
      }
    }

    protected override void InitalizeButton()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) {Name = "Refresh", Description = "刷新数据"});
      ButtonCollection.Add(new EsuButtonBase("增加", 0, Add) {Name = "Add", Description = "新增数据"});
      ButtonCollection.Add(new EsuButtonBase("删除", 0, Remove) {Name = "Delete", Description = "删除数据"});
      ButtonCollection.Add(new EsuButtonBase("保存", 0, Save) {Name = "Save", Description = "保存数据"});
    }

    protected virtual void Add()
    {
      Data.Collection.Add(InitailizeData());
    }

    protected virtual void Remove()
    {
      if (data.CurrentItem != null && Utils.DeleteMessage(EntityName))
        Data.Collection.Remove(data.CurrentItem);
    }

    protected abstract EsuDataEditCollection<T> GetData();

    protected virtual T InitailizeData()
    {
      return InitailizeDefaultData<T>();
    }

    public abstract void Save();
  }
}