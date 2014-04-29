using System.Data;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.DataOperations
{
  public interface IDataSave<in T>
  {
    IDataParameter[] MappingParameters(T data);

    string InsertSqlScript();

    string UpdateSqlScript();

    string DeleteSqlScript();
  }

  public interface IDataSaveLog<T> where T : EsuInfoBase
  {
    IDataParameter[] MappingParameters(ChangeData<T> data, string userID);

    string LogSqlScript();
  }
}