#region

using System.Windows;
using Supeng.Common.Controls;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows.Views;

#endregion

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class FindItemWindowViewModel<T> : DialogWindowBase where T : EsuInfoBase
  {
    private readonly EsuCommand clearCommand;
    private readonly EsuCommand searchCommand;
    private EsuInfoCollection<T> collection;
    private string searchText;

    protected FindItemWindowViewModel()
    {
      searchCommand = new EsuCommand(Search);
      clearCommand = new EsuCommand(Clear);
    }

    #region properties

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

    public string SearchText
    {
      get { return searchText; }
      set
      {
        if (value == searchText) return;
        searchText = value;
        NotifyOfPropertyChange(() => SearchText);
      }
    }

    #endregion

    #region commands
    public EsuCommand SearchCommand
    {
      get { return searchCommand; }
    }

    public EsuCommand ClearCommand
    {
      get { return clearCommand; }
    }
    #endregion

    public override FrameworkElement Content
    {
      get { return new FindItemControl(); }
    }

    public virtual FrameworkElement ResultContent
    {
      get { return new DefaultGridControl(); }
    }

    protected abstract void Search();

    protected virtual void Clear()
    {
      SearchText = string.Empty;
    }

    protected override string DataCheck()
    {
      if (collection.CurrentItem == null)
        return "请选择一项！";
      return string.Empty;
    }
  }
}