using Supeng.Common.Interfaces;

namespace Update
{
  public static class UpdateHelper
  {
    public static void ShowUpdate(string directoryName, IUpgrade upgrade)
    {
      var view = new UpdateWindowView
      {
        DataContext = new UpdateWindowViewModel(directoryName, upgrade)
      };
      view.Show();
    }
  }
}
