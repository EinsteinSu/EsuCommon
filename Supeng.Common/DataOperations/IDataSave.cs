using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.DataOperations
{
  public interface IDataSave<in T>
  {
    string InsertSqlScript(T data);

    string UpdateSqlScript(T data);

    string DeleteSqlScript(T data);
  }

  public interface IDataSaveLog<T> where T : EsuInfoBase
  {
    string DataOperationLog(ChangeData<T> data, string userID);
  }
}