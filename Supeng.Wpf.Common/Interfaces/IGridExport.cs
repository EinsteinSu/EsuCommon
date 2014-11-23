using System.Diagnostics;
using DevExpress.Xpf.Grid;
using Microsoft.Win32;

namespace Supeng.Wpf.Common.Interfaces
{
  public interface IGridExport
  {
    TableView View { get; }
  }

  public static class GridExportExtensions
  {
    public static void Export(this IGridExport grid)
    {
      if (grid.View == null)
        return;
      var sf = new SaveFileDialog { Filter = "Excel文件(*.xls)|*.xls" };
      var showDialog = sf.ShowDialog();
      if (showDialog != null && showDialog.Value)
      {
        grid.View.ExportToXls(sf.FileName);
        Process.Start("Explorer", "/select," + sf.FileName);
      }
    }
  }
}
