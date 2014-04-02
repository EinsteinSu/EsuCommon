using System.Windows;

namespace Supeng.Silverlight.Common
{
  public static class Utils
  {
    public static bool DeleteMessage(string deleteEntity)
    {
      return MessageBox.Show(string.Format("是否删除该[{0}]?", deleteEntity), "删除", MessageBoxButton.OKCancel) ==
             MessageBoxResult.OK;
    }
  }
}