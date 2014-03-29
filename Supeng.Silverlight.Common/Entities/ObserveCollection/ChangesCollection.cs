using System;
using System.Collections.ObjectModel;

namespace Supeng.Silverlight.Common.Entities.ObserveCollection
{
  public class ChangesCollection<T> : ObservableCollection<ChangeData<T>> where T : EsuInfoBase
  {
    public Action DataAdded { get; set; }

    protected override void InsertItem(int index, ChangeData<T> item)
    {
      base.InsertItem(index, item);
      if (DataAdded != null)
        DataAdded();
    }
  }
}