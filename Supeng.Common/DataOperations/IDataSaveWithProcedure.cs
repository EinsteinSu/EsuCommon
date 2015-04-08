using System.Data;

namespace Supeng.Common.DataOperations
{
  public interface IDataSaveWithProcedure<in T>
  {
    IDataParameter[] MappingParameters(T data);

    string GetProcedureName();
  }
}