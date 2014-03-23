namespace Supeng.Common.DataOperations
{
  public interface IDataSave<in T>
  {
    string InsertSql(T data);

    string UpdateSql(T data);

    string DeleteSql(T data);
  }
}
