namespace Supeng.Common.Interfaces
{
  public interface ICrudStorage<in T>
  {
    string Insert(T data);
    string Update(T data);
    string Delete(T data);
  }

  public interface ICrudWithParameterStorage<in T, in U>
  {
    string Insert(T data, U id);
    string Update(T data, U id);
    string Delete(T data, U id);
  }
}
