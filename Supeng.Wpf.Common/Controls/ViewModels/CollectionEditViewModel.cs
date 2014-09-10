using System.Diagnostics;
using System.Windows;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;
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
    private readonly CollectionEditGrid control;
    protected TableView TableView;
    protected CollectionEditViewModel()
    {
      InitalizeButton();
      control = new CollectionEditGrid();
      TableView = control.view;
    }

    public override FrameworkElement Content
    {
      get { return control; }
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
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) { Name = "Refresh", Description = "刷新数据" });
      ButtonCollection.Add(new EsuButtonBase("增加", 0, Add) { Name = "Add", Description = "新增数据" });
      ButtonCollection.Add(new EsuButtonBase("删除", 0, Remove) { Name = "Delete", Description = "删除数据" });
      ButtonCollection.Add(new EsuButtonBase("保存", 0, Save) { Name = "Save", Description = "保存数据" });
      ButtonCollection.Add(new EsuButtonBase("导出", 0, Export) { Name = "Export", Description = "导出至Excel" });
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

    protected virtual void Export()
    {
      var sf = new SaveFileDialog {Filter = "Excel文件(*.xls)|*.xls"};
      var showDialog = sf.ShowDialog();
      if (showDialog != null && showDialog.Value)
      {
        TableView.ExportToXls(sf.FileName);
        Process.Start("Explorer", "/select," + sf.FileName);
      }
    }

    public abstract void Save();
  }
}