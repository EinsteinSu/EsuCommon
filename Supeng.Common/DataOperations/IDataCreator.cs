using System.Data;

namespace Supeng.Common.DataOperations
{
  public interface IDataCreator<out T>
  {
    T CreateData(IDataReader reader);
  }
}
