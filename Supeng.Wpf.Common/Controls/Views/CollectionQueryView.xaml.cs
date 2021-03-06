﻿using DevExpress.Xpf.Grid;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.Views
{
  /// <summary>
  /// Interaction logic for CollectionQueryView.xaml
  /// </summary>
  public partial class CollectionQueryView : IGridExport
  {
    public CollectionQueryView()
    {
      InitializeComponent();
    }

    public TableView View
    {
      get { return view; }
    }
  }
}
