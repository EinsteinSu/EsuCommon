using System;
using System.Diagnostics;
using System.Windows;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;
using Supeng.Common.Controls;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;
using Supeng.Common.Threads;
using Supeng.Wpf.Common.Controls.Views;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class CollectionQueryControlViewModel<T> : ToolbarWithContentViewModelBase, IDataLoad where T : EsuInfoBase, new()
  {
    private readonly IProgress progress;
    private EsuInfoCollection<T> collection;
    private readonly CollectionQueryView view;
    protected CollectionQueryControlViewModel(IProgress progress)
    {
      this.progress = progress;
      view = new CollectionQueryView();
      InitalizeButton();
    }

    protected IProgress Progress
    {
      get { return progress; }
    }

    public override FrameworkElement Content
    {
      get { return view; }
    }

    protected virtual TableView TableView
    {
      get { return view.view; }
    }

    protected override void InitalizeButton()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) { Name = "Refresh", Description = "刷新数据" });
      ButtonCollection.Add(new EsuButtonBase("导出", 0, Export) { Name = "Export", Description = "导出至Excel" });
    }

    protected virtual void Export()
    {
      if (TableView == null)
        return;
      var sf = new SaveFileDialog { Filter = "Excel文件(*.xls)|*.xls" };
      var showDialog = sf.ShowDialog();
      if (showDialog != null && showDialog.Value)
      {
        TableView.ExportToXls(sf.FileName);
        Process.Start("Explorer", "/select," + sf.FileName);
      }
    }

    public EsuInfoCollection<T> Collection
    {
      get { return collection; }
    }

    protected abstract EsuInfoCollection<T> GetCollection();

    public void Load()
    {
      progress.ShowProgress("正在加载数据...");
      ThreadHelper.DoTask(GetCollection, result =>
      {
        collection = result;
        NotifyOfPropertyChange(() => Collection);
        progress.HideProgress();
      }, exceptions => progress.HideProgress());
    }
  }
}
