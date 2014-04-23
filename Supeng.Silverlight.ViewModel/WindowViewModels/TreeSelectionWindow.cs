using System.Windows.Controls;
using DevExpress.Xpf.Bars;
using Supeng.Silverlight.Common.Entities;
using Supeng.Silverlight.Common.Entities.BasesEntities;
using Supeng.Silverlight.Common.Entities.ObserveCollection;
using Supeng.Silverlight.Common.Interfaces;
using Supeng.Silverlight.ViewModel.Controls;

namespace Supeng.Silverlight.ViewModel.WindowViewModels
{
  public abstract class TreeSelectionViewModel<T> : EsuInfoBase, IDataLoad where T : TreeEntityBase, new()
  {
    private readonly DelegateCommand cancelCommand;
    private readonly DelegateCommand okCommand;
    private readonly EsuProgressViewModel progress;
    private EsuInfoCollection<T> collection;
    private T currentItem;
    private ChildWindow window;

    protected TreeSelectionViewModel()
    {
      okCommand = new DelegateCommand(OkClick, () => true);
      cancelCommand = new DelegateCommand(CancelClick, () => true);
      progress = new EsuProgressViewModel();
    }

    public ChildWindow Window
    {
      get { return window; }
      set
      {
        if (Equals(value, window)) return;
        window = value;
        NotifyOfPropertyChange(() => Window);
      }
    }

    public EsuProgressViewModel Progress
    {
      get { return progress; }
    }

    public EsuInfoCollection<T> Collection
    {
      get { return collection; }
      set
      {
        if (Equals(value, collection)) return;
        collection = value;
        NotifyOfPropertyChange(() => Collection);
      }
    }

    public T CurrentItem
    {
      get { return currentItem; }
      set
      {
        if (Equals(value, currentItem)) return;
        currentItem = value;
        NotifyOfPropertyChange(() => CurrentItem);
      }
    }

    public DelegateCommand OkCommand
    {
      get { return okCommand; }
    }

    public DelegateCommand CancelCommand
    {
      get { return cancelCommand; }
    }

    public abstract string Title { get; }

    public void Load()
    {
      Collection = GetCollection();
    }

    protected virtual void OkClick()
    {
      if (currentItem != null)
      {
        window.DialogResult = true;
      }
    }

    protected virtual void CancelClick()
    {
      window.DialogResult = false;
    }

    public abstract EsuInfoCollection<T> GetCollection();
  }
}