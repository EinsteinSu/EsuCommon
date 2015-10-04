using System.Collections.Generic;
using System.Linq;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows.Models;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class FindItemMultiWindowViewModel<T> : FindItemWindowViewModel<SelectionModel<T>>
    where T : EsuInfoBase
  {
    public override void Load()
    {
      base.Load();
      ConvertCollection(GetCollection());
    }

    protected void ConvertCollection(EsuInfoCollection<T> collection)
    {
      Collection = new EsuInfoCollection<SelectionModel<T>>();
      foreach (T item in collection)
      {
        Collection.Add(new SelectionModel<T>(item));
      }
      NotifyOfPropertyChange(() => Collection);
    }

    public IList<SelectionModel<T>> SelectionDataCollection
    {
      get { return Collection.Where(s => s.Selected).ToList(); }
    }

    protected override string DataCheck()
    {
      if (!SelectionDataCollection.Any())
      {
        return "请勾选一个选项！";
      }
      return string.Empty;
    }

    protected abstract EsuInfoCollection<T> GetCollection();
  }
}