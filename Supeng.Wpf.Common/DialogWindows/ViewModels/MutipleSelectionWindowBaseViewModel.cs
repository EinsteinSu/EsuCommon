using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Wpf.Common.Commands;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class MutipleSelectionWindowBaseViewModel<T> : DialogWindowBase
  {
    private readonly IList<T> existData;
    private List<MutiplySelectionEntityBase<T>> data;
    private readonly List<T> dataResult;
    private readonly MutiplySelectionCommand<T> selectionCommands;

    protected MutipleSelectionWindowBaseViewModel(IList<T> existData = null)
    {
      this.existData = existData;
      data = new List<MutiplySelectionEntityBase<T>>();
      dataResult = new List<T>();
      selectionCommands = new MutiplySelectionCommand<T>(data);
    }

    protected abstract IList<T> GetData();

    public virtual MutiplySelectionCommand<T> SelectionCommands
    {
      get { return selectionCommands; }
    }

    public List<MutiplySelectionEntityBase<T>> Data
    {
      get { return data; }
      set
      {
        if (Equals(value, data)) return;
        data = value;
        NotifyOfPropertyChange(() => Data);
      }
    }

    public List<T> DataResult
    {
      get { return dataResult; }
    }

    protected virtual bool GetExistData(T item, out MutiplySelectionEntityBase<T> result)
    {
      result = data.FirstOrDefault(f => f.Data.Equals(item));
      if (result != null)
        return true;
      return false;
    }

    public override void Load()
    {
      base.Load();
      foreach (var item in GetData())
      {
        data.Add(new MutiplySelectionEntityBase<T> { Selected = false, Data = item });
      }
      if (existData != null)
      {
        foreach (var item in existData)
        {
          MutiplySelectionEntityBase<T> result;
          if (GetExistData(item, out result))
            result.Selected = true;
        }
      }
      NotifyOfPropertyChange(() => Data);
    }

    public override FrameworkElement Content
    {
      get { return new MutipleSelectionControl(); }
    }

    protected override string DataCheck()
    {
      dataResult.AddRange(data.Where(w => w.Selected).Select(s => s.Data));
      if (!dataResult.Any())
        return "请选择一项数据";
      return string.Empty;
    }
  }
}
