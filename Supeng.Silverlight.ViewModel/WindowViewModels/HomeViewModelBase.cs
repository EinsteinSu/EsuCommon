using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.BasesEntities.DataEntities;
using Supeng.Silverlight.Common.Entities.ControlEntities.NavBarGroupItem;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.Common.Interfaces.Controls;
using Supeng.Silverlight.ViewModel.Controls;

namespace Supeng.Silverlight.ViewModel.WindowViewModels
{
  public abstract class HomeViewModelBase : EsuInfoBase
  {
    private UserControlFunctionItem<ApplicationFunction> currentUserControl;
    private ObservableCollection<EsuDisplayNavBarGroup<ApplicationFunction>> functionCollection;
    private UserControlFunctionItemCollection<ApplicationFunction> openedUserControlCollection;

    protected HomeViewModelBase()
    {
      openedUserControlCollection = new UserControlFunctionItemCollection<ApplicationFunction>();
    }

    #region function

    public virtual ObservableCollection<EsuDisplayNavBarGroup<ApplicationFunction>> FunctionCollection
    {
      get
      {
        return functionCollection ??
               (functionCollection = new ObservableCollection<EsuDisplayNavBarGroup<ApplicationFunction>>());
      }
    }

    public virtual void FunctionClick(ApplicationFunction function)
    {
      UserControlFunctionItem<ApplicationFunction> first =
        openedUserControlCollection.FirstOrDefault(f => f.Data.ID == function.ID);
      if (first != null)
      {
        CurrentUserControl = first;
        return;
      }
      var control = new UserControlFunctionItem<ApplicationFunction>(function.ImageUrl, CloseFunction)
      {
        Header = function.Name,
        Data = function,
        Content = GetContentFromFunction(function),
      };

      var showProgress = control.Content.DataContext as IProgress;
      if (showProgress != null)
      {
        showProgress.StartShowing = s => control.Progress.ShowProgress(string.Format("Loading {0}", function.Name));
        showProgress.StopShowing = () => control.Progress.HideProgress();
      }

      var dataLoad = control.Content as IDataLoad;
      if (dataLoad != null)
      {
        dataLoad.Load();
      }

      openedUserControlCollection.Add(control);
      CurrentUserControl = control;
      NotifyOfPropertyChange(() => OpenedUserControlCollection);
    }

    #endregion

    #region opened user control

    public UserControlFunctionItemCollection<ApplicationFunction> OpenedUserControlCollection
    {
      get { return openedUserControlCollection; }
      set
      {
        if (Equals(value, openedUserControlCollection)) return;
        openedUserControlCollection = value;
        NotifyOfPropertyChange(() => OpenedUserControlCollection);
      }
    }

    public UserControlFunctionItem<ApplicationFunction> CurrentUserControl
    {
      get { return currentUserControl; }
      set
      {
        if (Equals(value, currentUserControl)) return;
        currentUserControl = value;
        NotifyOfPropertyChange(() => CurrentUserControl);
      }
    }

    public void CloseFunction(ApplicationFunction function)
    {
      UserControlFunctionItem<ApplicationFunction> first =
        openedUserControlCollection.FirstOrDefault(f => f.Data.ID == function.ID);
      if (first != null)
      {
        //test if exist when the collection removed
        var dispose = first.Content as IDisposable;
        if (dispose != null)
          dispose.Dispose();
        openedUserControlCollection.Remove(first);
      }
    }

    #endregion

    public abstract string Title { get; }

    public abstract ImageSource Image { get; }

    public abstract FrameworkElement GetContentFromFunction(ApplicationFunction function);
  }
}