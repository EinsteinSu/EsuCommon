using System;
using System.Linq;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;

namespace Supeng.Common.Entities.Utilitis
{
  public static class TreeCollectionUtility
  {
    public static void Check<T>(this T data, EsuInfoCollection<T> collection) where T : TreeEntityBase, IChecked
    {
      foreach (T item in collection.Where(w => w.PID.Equals(data.ID)))
      {
        item.IsChecked = data.IsChecked;
        item.IsNotifying = true;
        item.NotifyOfPropertyChange("IsChecked");
        Check(item, collection);
      }
    }
  }
}