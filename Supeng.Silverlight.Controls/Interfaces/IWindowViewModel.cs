using DevExpress.Xpf.Core;

namespace Supeng.Silverlight.Controls.Interfaces
{
  public interface IWindowViewModel
  {
    string Title { get; }

    int Height { get; }

    int Width { get; }

    DXWindow Window { get; set; }
  }
}