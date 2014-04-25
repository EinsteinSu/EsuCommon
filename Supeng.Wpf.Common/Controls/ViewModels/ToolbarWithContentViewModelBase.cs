using System.Windows;
using Supeng.Common.Entities;
using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Controls.ViewModels
{
  public abstract class ToolbarWithContentViewModelBase : EsuInfoBase
  {
    private readonly EsuToolBarButtonCollection buttonCollection;

    protected ToolbarWithContentViewModelBase()
    {
      // ReSharper disable once DoNotCallOverridableMethodsInConstructor
      buttonCollection = new EsuToolBarButtonCollection(ToolBarType);
    }

    protected abstract void InitalizeButton();

    public abstract FrameworkElement Content { get; }

    public EsuToolBarButtonCollection ButtonCollection
    {
      get { return buttonCollection; }
    }

    protected virtual EsuButtonToolBarType ToolBarType
    {
      get
      {
        return EsuButtonToolBarType.OnlyImage;
      }
    }
  }
}
