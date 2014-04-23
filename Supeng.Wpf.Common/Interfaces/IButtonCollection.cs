using Supeng.Wpf.Common.Entities;

namespace Supeng.Wpf.Common.Interfaces
{
  public interface IButtonCollection
  {
    EsuToolBarButtonCollection ButtonCollection { get; }

    void InitalizeButtonCollection();
  }
}