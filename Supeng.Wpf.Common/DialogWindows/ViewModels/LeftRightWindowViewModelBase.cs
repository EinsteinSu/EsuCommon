using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Bars;
using DevExpress.Xpf.Grid;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Threads;
using Supeng.Wpf.Common.DialogWindows.Views;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class LeftRightWindowViewModelBase<T> : DialogWindowBase where T : EsuInfoBase
  {
    private readonly DelegateCommand leftAllClickCommand;
    private readonly DelegateCommand leftClickCommand;
    private readonly DelegateCommand rightAllClickCommand;
    private readonly DelegateCommand rightClickCommand;
    private readonly TaskScheduler scheduler;
    private EsuInfoCollection<T> leftCollection;
    private EsuInfoCollection<T> rightCollection;

    protected LeftRightWindowViewModelBase()
    {
      scheduler = TaskScheduler.Current;
      leftClickCommand = new DelegateCommand(LeftClick, () => true);
      leftAllClickCommand = new DelegateCommand(LeftAllClick, () => true);
      rightClickCommand = new DelegateCommand(RightClick, () => true);
      rightAllClickCommand = new DelegateCommand(RightAllClick, () => true);
      leftCollection = new EsuInfoCollection<T>();
      rightCollection = new EsuInfoCollection<T>();
    }

    protected virtual Action RightCollectionChanged
    {
      get { return null; }
    }

    public override FrameworkElement Content
    {
      get
      {
        return new LeftRightGridControl();
      }
    }

    public virtual ShowSearchPanelMode FindMode
    {
      get
      {
        return ShowSearchPanelMode.Always;
      }
    }

    #region properties

    public EsuInfoCollection<T> LeftCollection
    {
      get { return leftCollection; }
    }

    public bool LeftButtonEnable
    {
      get { return leftCollection.CurrentItem != null; }
    }

    public bool LeftAllButtonEnable
    {
      get { return leftCollection != null && leftCollection.Any(); }
    }

    public EsuInfoCollection<T> RightCollection
    {
      get { return rightCollection; }
    }

    public bool RightButtonEnable
    {
      get { return rightCollection.CurrentItem != null; }
    }

    public bool RightAllButtonEnable
    {
      get { return rightCollection != null && rightCollection.Any(); }
    }

    #endregion

    protected abstract EsuInfoCollection<T> InitalizeLeftCollection();

    protected abstract EsuInfoCollection<T> InitalizeRightCollection();

    public override async void Load()
    {
      base.Load();
      Progress.ShowProgress("正在加载数据 ...");
      await ThreadHelper.StartTask(() =>
      {
        leftCollection = InitalizeLeftCollection();
        rightCollection = InitalizeRightCollection();
      });
      NotifyOfPropertyChange(() => LeftCollection);
      NotifyOfPropertyChange(() => RightCollection);
      leftCollection.CurrentItemChanged = obj => NotifyOfPropertyChange(() => LeftButtonEnable);
      rightCollection.CurrentItemChanged = obj => NotifyOfPropertyChange(() => RightButtonEnable);
      Progress.HideProgress();
    }

    protected override string DataCheck()
    {
      return string.Empty;
    }

    #region commands

    public DelegateCommand RightClickCommand
    {
      get { return rightClickCommand; }
    }

    public DelegateCommand RightAllClickCommand
    {
      get { return rightAllClickCommand; }
    }

    public DelegateCommand LeftClickCommand
    {
      get { return leftClickCommand; }
    }

    public DelegateCommand LeftAllClickCommand
    {
      get { return leftAllClickCommand; }
    }

    #endregion

    #region button left right click

    protected virtual void LeftClick()
    {
      if (rightCollection.CurrentItem != null)
      {
        LeftCollection.Add(rightCollection.CurrentItem);
        RightCollection.Remove(rightCollection.CurrentItem);
        NotifyOfPropertyChange(() => LeftAllButtonEnable);
        NotifyOfPropertyChange(() => RightAllButtonEnable);
        if (RightCollectionChanged != null)
          RightCollectionChanged();
      }
    }

    protected virtual void LeftAllClick()
    {
      while (true)
      {
        if (rightCollection.Any())
        {
          T item = RightCollection[0];
          RightCollection.Remove(item);
          LeftCollection.Add(item);
          NotifyOfPropertyChange(() => LeftAllButtonEnable);
          NotifyOfPropertyChange(() => RightAllButtonEnable);
        }
        else
          break;
      }
      if (RightCollectionChanged != null)
        RightCollectionChanged();
    }

    protected virtual void RightClick()
    {
      if (leftCollection.CurrentItem != null)
      {
        RightCollection.Add(leftCollection.CurrentItem);
        LeftCollection.Remove(leftCollection.CurrentItem);
        NotifyOfPropertyChange(() => LeftAllButtonEnable);
        NotifyOfPropertyChange(() => RightAllButtonEnable);
        if (RightCollectionChanged != null)
          RightCollectionChanged();
      }
    }

    protected virtual void RightAllClick()
    {
      while (true)
      {
        if (leftCollection.Any())
        {
          T item = LeftCollection[0];
          LeftCollection.Remove(item);
          RightCollection.Add(item);
          NotifyOfPropertyChange(() => LeftAllButtonEnable);
          NotifyOfPropertyChange(() => RightAllButtonEnable);
        }
        else
          break;
      }
      if (RightCollectionChanged != null)
        RightCollectionChanged();
    }

    #endregion
  }
}