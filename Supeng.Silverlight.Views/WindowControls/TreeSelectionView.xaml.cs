using System.Windows;
using System.Windows.Controls;

namespace Supeng.Silverlight.Views.WindowControls
{
  public partial class TreeSelectionView : ChildWindow
  {
    public TreeSelectionView()
    {
      InitializeComponent();
    }

    private void OKButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = true;
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      DialogResult = false;
    }
  }
}