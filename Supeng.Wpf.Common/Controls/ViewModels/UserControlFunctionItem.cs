using System;
using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.ControlEntities.NavBarGroupItem;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public class UserControlFunctionItemBase<T, TO> : EsuDisplayItem<T>
  {
    private TO content;
    private EsuProgressViewModel progress;

    public UserControlFunctionItemBase(string imagePath, Action<T> action)
      : base(imagePath, action)
    {
      progress = new EsuProgressViewModel();
    }

    public UserControlFunctionItemBase()
    {
    }

    public ImageSource CloseImage
    {
      get
      {
        string fileName = string.Format("{0}\\Images\\CloseButton16.png", Environment.CurrentDirectory);
        if (File.Exists(fileName))
          return new BitmapImage(new Uri(fileName, UriKind.Absolute));
        return null;
      }
    }

    public TO Content
    {
      get { return content; }
      set
      {
        if (Equals(value, content)) return;
        content = value;
        NotifyOfPropertyChange(() => Content);
      }
    }

    public EsuProgressViewModel Progress
    {
      get { return progress; }
      set
      {
        if (Equals(value, progress)) return;
        progress = value;
        NotifyOfPropertyChange(() => Progress);
      }
    }
  }

  public class UserControlFunctionItem<T> : UserControlFunctionItemBase<T, FrameworkElement>
  {
    public UserControlFunctionItem(string imagePath, Action<T> action)
      : base(imagePath, action)
    {
    }

    public UserControlFunctionItem()
    {
    }
  }

  public class UserControlFunctionItemCollection<T> : EsuInfoCollection<UserControlFunctionItem<T>>
  {
  }
}