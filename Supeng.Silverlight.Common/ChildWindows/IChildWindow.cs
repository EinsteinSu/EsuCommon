namespace Supeng.Silverlight.Common.ChildWindows
{
  public interface IChildWindow<T>
  {
    string Title { get; }

    T Data { get; set; }
  }
}