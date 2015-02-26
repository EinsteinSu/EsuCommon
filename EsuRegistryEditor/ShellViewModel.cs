using System.Windows;
using Supeng.Common.IOs;

namespace EsuRegistryEditor {
  public class ShellViewModel : Caliburn.Micro.PropertyChangedBase, IShell
  {
    private EsuRegistry registry;
    private string prefix;

    public string Prefix
    {
      get { return prefix; }
      set
      {
        if (value == prefix) return;
        prefix = value;
        NotifyOfPropertyChange(() => Prefix);
      }
    }

    public EsuRegistry Registry
    {
      get { return registry; }
      set
      {
        if (Equals(value, registry)) return;
        registry = value;
        NotifyOfPropertyChange(() => Registry);
      }
    }

    public void Load()
    {
      if (string.IsNullOrEmpty(prefix))
      {
        MessageBox.Show("请填写前缀!");
        return;
      }
      //registry = new EsuRegistry(prefix);
    }
  }
}