using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities.ObserveCollection;

namespace Supeng.Silverlight.Common.Entities.ControlEntities.NavBarGroupItem
{
  public class EsuDisplayNavBarGroup<T> : EsuDisplayItem<T>
  {
    private EsuInfoCollection<EsuDisplayNavBarGroup<T>> child;

    public EsuDisplayNavBarGroup()
    {
    }

    public EsuDisplayNavBarGroup(string imagePath, Action<T> action)
      : base(imagePath, action)
    {
      child = new EsuInfoCollection<EsuDisplayNavBarGroup<T>>();
    }

    public EsuInfoCollection<EsuDisplayNavBarGroup<T>> Child
    {
      get { return child; }
      set
      {
        if (Equals(value, child)) return;
        child = value;
        NotifyOfPropertyChange(() => Child);
      }
    }
  }

  public class EsuDisplayItem<T> : EsuInfoBase
  {
    private readonly Action<T> action;
    private readonly DelegateCommand clickCommand;
    private readonly string imagePath;

    public EsuDisplayItem()
    {
    }

    public EsuDisplayItem(string imagePath, Action<T> action)
    {
      this.imagePath = imagePath;
      this.action = action;
      clickCommand = new DelegateCommand(ExecuteClick, () => true);
    }

    public DelegateCommand ClickCommand
    {
      get { return clickCommand; }
    }

    public string Header { get; set; }

    public ImageSource Image
    {
      get { return !string.IsNullOrEmpty(imagePath) ? new BitmapImage(new Uri(imagePath, UriKind.Relative)) : null; }
    }

    public T Data { get; set; }

    public void ExecuteClick()
    {
      if (action != null)
        action(Data);
    }
  }
}