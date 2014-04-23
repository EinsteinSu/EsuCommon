using System.Windows;

namespace Supeng.Wpf.Common.Interfaces
{
  public interface IWindowViewModel
  {
    string Title { get; }

    int Height { get; }

    int Width { get; }

    Window Window { get; set; }
  }
}
