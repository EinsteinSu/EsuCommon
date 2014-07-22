using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
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
      get { return new LeftRightGridControl(); }
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

    public override void Load()
    {
      base.Load();
      Progress.ShowProgress("正在加载数据 ...");
      Task task = Task.Factory.StartNew(() =>
      {
        leftCollection = InitalizeLeftCollection();
        rightCollection = InitalizeRightCollection();
      });
      task.ContinueWith(t =>
      {
        NotifyOfPropertyChange(() => LeftCollection);
        NotifyOfPropertyChange(() => RightCollection);
        leftCollection.CurrentItemChanged = obj => NotifyOfPropertyChange(() => LeftButtonEnable);
        rightCollection.CurrentItemChanged = obj => NotifyOfPropertyChange(() => RightButtonEnable);
        Progress.HideProgress();
      }, CancellationToken.None, TaskContinuationOptions.OnlyOnRanToCompletion, scheduler);
      task.ContinueWith(t =>
      {
        Progress.HideProgress();
        MessageBox.Show("加载数据错误！");
      }, CancellationToken.None, TaskContinuationOptions.OnlyOnFaulted, scheduler);
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