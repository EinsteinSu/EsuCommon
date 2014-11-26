using System.Windows;
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

    public abstract SearchControlViewModel Search { get; }
  }
}
