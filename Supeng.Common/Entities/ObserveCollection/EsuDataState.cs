using Supeng.Common.Types;

namespace Supeng.Common.Entities.ObserveCollection
{
  public enum EsuDataState
  {
    Added,
    Modified,
    Deleted
  }

  public static class EsuDataStateExtensions
  {
    public static EsuDataState GetState(this string state)
    {
      return state.EnumConvert<EsuDataState>();
    }
  }
}