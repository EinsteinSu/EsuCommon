using System.Windows;
using Supeng.Common;
using Supeng.Common.Entities;
using Supeng.Data;
using Supeng.Wpf.Common.DialogWindows.Views;
using Supeng.Wpf.Common.Entities;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class CollectionEditWindowViewModel<T> : DialogWindowBase, IButtonCollection where T : EsuInfoBase, new()
  {
    private readonly EsuToolBarButtonCollection buttonCollection;
    private EsuDataEditCollection<T> data;

    protected CollectionEditWindowViewModel()
    {
      buttonCollection = new EsuToolBarButtonCollection(ToolBarType);
      InitalizeButtonCollection();
    }

    public abstract string EntityName { get; }

    protected abstract EsuDataEditCollection<T> GetData();

    public override void Load()
    {
      data = GetData();
      if (data != null)
      {
        data.Load();
        NotifyOfPropertyChange(() => Data);
      }
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

    protected virtual T InitailizeData()
    {
      return InitailizeDefaultData<T>();
    }

    public EsuToolBarButtonCollection ButtonCollection
    {
      get { return buttonCollection; }
    }

    public void InitalizeButtonCollection()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) { Name = "Refresh", Description = "刷新数据" });
      ButtonCollection.Add(new EsuButtonBase("增加", 0, Add) { Name = "Add", Description = "新增数据" });
      ButtonCollection.Add(new EsuButtonBase("删除", 0, Remove) { Name = "Delete", Description = "删除数据" });
    }

    protected virtual EsuButtonToolBarType ToolBarType
    {
      get
      {
        return EsuButtonToolBarType.OnlyImage;
      }
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

    public virtual DataTemplate ItemTemplate
    {
      get { return null; }
    }

    public override FrameworkElement Content
    {
      get { return new ListEditControl(); }
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }
  }
}