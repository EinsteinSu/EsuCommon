using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Supeng.Common.Entities;
using Supeng.Common.Entities.BasesEntities.DataEntities;
using Supeng.Common.Entities.ControlEntities.NavBarGroupItem;
using Supeng.Common.Interfaces;
using Supeng.Wpf.Common.Controls.ViewModels;

namespace Supeng.Wpf.Common
{
  public abstract class HomeViewModelBase : EsuInfoBase, IDisposable
  {
    private readonly EsuProgressViewModel progress;
    private UserControlFunctionItem<ApplicationFunction> currentUserControl;
    private ObservableCollection<EsuDisplayNavBarGroup<ApplicationFunction>> functionCollection;
    private UserControlFunctionItemCollection<ApplicationFunction> openedUserControlCollection;

    protected HomeViewModelBase()
    {
      openedUserControlCollection = new UserControlFunctionItemCollection<ApplicationFunction>();
      progress = new EsuProgressViewModel();
    }

    #region function

    public virtual ObservableCollection<EsuDisplayNavBarGroup<ApplicationFunction>> FunctionCollection
    {
      get { return functionCollection; }
      protected set
      {
        if (Equals(value, functionCollection)) return;
        functionCollection = value;
        NotifyOfPropertyChange(() => FunctionCollection);
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
        Data = function
      };
      control.Content = GetContentFromFunction(function, control.Progress);

      var dataLoad = control.Content as IDataLoad;
      if (dataLoad != null)
        dataLoad.Load();

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

    public EsuProgressViewModel Progress
    {
      get { return progress; }
    }

    public abstract string Title { get; }

    public abstract ImageSource Image { get; }

    public void Dispose()
    {
      if (openedUserControlCollection != null)
      {
        foreach (var functionItem in openedUserControlCollection)
        {
          var dispose = functionItem.Content as IDisposable;
          if (dispose != null)
            dispose.Dispose();
        }
      }
    }

    public abstract FrameworkElement GetContentFromFunction(ApplicationFunction function, EsuProgressViewModel progress);
  }
}