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
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class CollectionQueryControlViewModel<T> : ToolbarWithContentViewModelBase, IDataLoad where T : EsuInfoBase, new()
  {
    private readonly IProgress progress;
    private EsuInfoCollection<T> collection;
    private CollectionQueryView view;
    protected CollectionQueryControlViewModel(IProgress progress)
    {
      this.progress = progress;
      InitalizeButton();
    }

    protected IProgress Progress
    {
      get { return progress; }
    }

    public override FrameworkElement Content
    {
      get { return view ?? (view = new CollectionQueryView()); }
    }

    protected virtual IGridExport GridExport
    {
      get { return Content as IGridExport; }
    }

    protected override void InitalizeButton()
    {
      ButtonCollection.Add(new EsuButtonBase("刷新", 0, Load) { Name = "Refresh", Description = "刷新数据" });
      ButtonCollection.Add(new EsuButtonBase("导出", 0, Export) { Name = "Export", Description = "导出至Excel" });
    }

    protected virtual void Export()
    {
      if (GridExport != null)
        GridExport.Export();
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
