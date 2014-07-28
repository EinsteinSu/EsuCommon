using System.Windows;
using System.Windows.Controls;
using DevExpress.Xpf.Editors;

namespace Supeng.Silverlight.Controls.Extensions
{
  public class EsuMemoText : TextEdit
  {
    public EsuMemoText()
    {
      AcceptsReturn = true;
      HorizontalContentAlignment = HorizontalAlignment.Left;
      VerticalContentAlignment = VerticalAlignment.Top;
      VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
      HorizontalScrollBarVisibility = ScrollBarVisibility.Disabled;
    }
  }
}
