using DevExpress.Xpf.Grid;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.Views
{
  /// <summary>
  /// Interaction logic for CollectionQueryWithSearchView.xaml
  /// </summary>
  public partial class CollectionQueryWithSearchView : IGridExport
  {
    public CollectionQueryWithSearchView()
    {
      InitializeComponent();
    }

    public TableView View
    {
      get { return view; }
    }
  }
}
