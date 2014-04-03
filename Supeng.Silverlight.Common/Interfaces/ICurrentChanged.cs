namespace Supeng.Silverlight.Common.Interfaces
{
  public interface ICurrentChange<in T>
  {
    void CurrentChanged(T data);
  }
}
