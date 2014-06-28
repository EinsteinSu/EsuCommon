using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class SearchControlWithDataLayoutViewModel : EsuInfoBase
  {
    private readonly DelegateCommand clearCommand;
    private readonly DelegateCommand searchCommand;

    protected SearchControlWithDataLayoutViewModel()
    {
      searchCommand = new DelegateCommand(Search, () => true);
      clearCommand = new DelegateCommand(Clear, () => true);
    }

    public abstract ISearchModel SearchModel { get; set; }

    public DelegateCommand SearchCommand
    {
      get { return searchCommand; }
    }

    public DelegateCommand ClearCommand
    {
      get { return clearCommand; }
    }

    protected virtual void Search()
    {
    }

    protected virtual void Clear()
    {
      SearchModel.Clear();
    }
  }
}