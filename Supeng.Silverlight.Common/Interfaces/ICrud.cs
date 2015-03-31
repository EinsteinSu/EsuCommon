namespace Supeng.Silverlight.Common.Interfaces
{
  public interface ICrud<in T>
  {
    void Create();

    void Update(T data);

    void Delete(T data);
  }
}
