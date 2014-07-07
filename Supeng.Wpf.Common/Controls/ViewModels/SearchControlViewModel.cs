using System.Windows;
using DevExpress.Xpf.Bars;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Interfaces;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class SearchControlViewModel : EsuInfoBase
  {
    private readonly DelegateCommand clearCommand;
    private readonly DelegateCommand searchCommand;

    protected SearchControlViewModel()
    {
      searchCommand = new DelegateCommand(Search, () => true);
      clearCommand = new DelegateCommand(Clear, () => true);
    }

    public abstract FrameworkElement Content { get; }

    public abstract ISearchModel Data { get; set; }

    public DelegateCommand SearchCommand
    {
      get { return searchCommand; }
    }

    public DelegateCommand ClearCommand
    {
      get { return clearCommand; }
    }

    protected abstract void Search();

    protected virtual void Clear()
    {
      if (Data != null)
        Data.Clear();
    }
  }
}