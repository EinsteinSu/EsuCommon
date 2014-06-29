using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.ControlEntities;
using Supeng.Silverlight.Common.Interfaces.Controls;

namespace Supeng.Silverlight.ViewModel.Controls.ManagementControls
{
  public abstract class ManagementWithToolbarViewModelBase : EsuInfoBase, IShowProgress
  {
    private readonly ObservableCollection<EsuToolbarButton> buttonCollection;

    protected ManagementWithToolbarViewModelBase()
    {
      buttonCollection = new ObservableCollection<EsuToolbarButton>();
    }

    protected virtual string ApplicationName
    {
      get { return string.Empty; }
    }

    public ObservableCollection<EsuToolbarButton> ButtonCollection
    {
      get { return buttonCollection; }
    }

    protected MessageBoxResult RefreshBoxResult
    {
      get { return MessageBox.Show("数据已发生更改，是否保存数据？", "刷新数据", MessageBoxButton.OKCancel); }
    }

    public Action<string> StartShowing { get; set; }

    public Action StopShowing { get; set; }

    protected virtual ImageSource GetImageSourceByName(string name)
    {
      string fileName = string.Format("/images/Buttons/{0}.png", name);
      return new BitmapImage(new Uri(fileName, UriKind.Relative));
    }

    public virtual void InsertButton(EsuToolbarButton button, int index)
    {
      buttonCollection.Insert(index, button);
      NotifyOfPropertyChange(() => ButtonCollection);
    }

    public virtual void AddButton(EsuToolbarButton button)
    {
      InsertButton(button, buttonCollection.Count);
    }

    protected abstract void InitalizeButtons();
  }
}