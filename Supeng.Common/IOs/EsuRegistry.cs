using System;
using System.Collections.Generic;
using Microsoft.Win32;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

namespace Supeng.Common.IOs
{
  public class EsuRegistry : EsuInfoBase
  {
    private EsuInfoCollection<EsuInfoBase> registries;

    public EsuRegistry(IEnumerable<string> keys)
    {
      registries = new EsuInfoCollection<EsuInfoBase>();
      foreach (var key in keys)
      {
        var value = Registry.LocalMachine.GetValue(key) ?? "";
        registries.Add(new EsuInfoBase { ID = key, Description = value.ToString() });
      }
    }

    public EsuInfoCollection<EsuInfoBase> Registries
    {
      get { return registries; }
      set
      {
        if (Equals(value, registries)) return;
        registries = value;
        NotifyOfPropertyChange(() => Registries);
      }
    }

    public void Save()
    {
      foreach (var registry in registries)
      {
        Registry.LocalMachine.SetValue(registry.ID, registry.Description);
      }
    }
  }

  public static class EsuRegistryExtensions
  {
    public static string GetDataFromLocalMachine(string key, string defaultData)
    {
      var data = Registry.LocalMachine.GetValue(key);
      if (data == null || string.IsNullOrEmpty(data.ToString()))
      {
        try
        {
          Registry.LocalMachine.SetValue(key, defaultData);
          data = defaultData;
        }
        catch
        {
          return defaultData;
        }
      }
      return data.ToString();
    }
  }
}
