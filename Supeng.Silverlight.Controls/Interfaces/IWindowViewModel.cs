using System.Windows.Controls;

namespace Supeng.Silverlight.Controls.Interfaces
{
  public interface IWindowViewModel
  {
    string Title { get; }

    int Height { get; }

    int Width { get; }

    ChildWindow Window { get; set; }
  }
}