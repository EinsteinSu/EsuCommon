using System.Collections.Generic;
using System.Linq;
using System.Windows;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Printing.Native;
using Supeng.Common.Controls;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Controls.Views;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class CollectionQueryWithSearchControlViewModel<T> : CollectionQueryControlViewModel<T> where T : EsuInfoBase, new()
  {
    protected CollectionQueryWithSearchControlViewModel(IProgress progress)
      : base(progress)
    {
    }

    private CollectionQueryWithSearchView view;
    public override FrameworkElement Content
    {
      get { return view ?? (view = new CollectionQueryWithSearchView()); }
    }

    public bool ShowSummary
    {
      get { return Summaries.Any(); }
    }

    public virtual List<GridSummaryItem> Summaries
    {
      get
      {
        return new List<GridSummaryItem>();
      }
    }

    public abstract SearchControlViewModel Search { get; }
  }
}
