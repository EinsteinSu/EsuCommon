using DevExpress.Xpf.Bars;
using System;
using System.IO;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Supeng.Silverlight.Common.Entities.ControlEntities
{
  public class EsuToolbarButton : EsuInfoBase
  {
    private DelegateCommand command;
    private bool enable = true;
    private Thickness margin = new Thickness(0);
    public string Title { get; set; }

    public EsuToolbarButton()
    {

    }

    public EsuToolbarButton(int left)
    {
      margin = new Thickness(left, 0, 0, 0);
    }

    public string ToolTip { get; set; }

    public Thickness Margin
    {
      get { return margin; }
      set
      {
        if (value.Equals(margin)) return;
        margin = value;
        NotifyOfPropertyChange(() => Margin);
      }
    }

    public bool Enable
    {
      get { return enable; }
      set
      {
        if (value.Equals(enable)) return;
        enable = value;
        NotifyOfPropertyChange(() => Enable);
      }
    }

    public DelegateCommand Command
    {
      get { return command; }
      set
      {
        if (Equals(value, command)) return;
        command = value;
        NotifyOfPropertyChange(() => Command);
      }
    }

    public ImageSource Image { get; set; }

    public static ImageSource GetImageSourceByName(string name)
    {
      string fileName = string.Format("{0}\\images\\Button\\{1}.png", Environment.CurrentDirectory, name);
      if (File.Exists(fileName))
        return new BitmapImage(new Uri(fileName, UriKind.Absolute));
      return null;
    }
  }
}
