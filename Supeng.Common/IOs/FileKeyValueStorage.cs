#region

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Interfaces;
using Supeng.Common.Strings;

#endregion

namespace Supeng.Common.IOs
{
  public class FileKeyValueStorage : EsuInfoBase, IKeyValueStorage
  {
    private readonly string fileName;
    private readonly IEnumerable<string> keys;
    private EsuInfoCollection<EsuKeyValue> keyValues;

    public FileKeyValueStorage(string fileName, IEnumerable<string> keys = null)
    {
      this.fileName = fileName;
      this.keys = keys;
    }

    public EsuInfoCollection<EsuKeyValue> KeyValues
    {
      get
      {
        if (keyValues == null)
          Load();
        return keyValues;
      }
      set
      {
        if (Equals(value, keyValues)) return;
        keyValues = value;
        NotifyOfPropertyChange(() => KeyValues);
      }
    }

    public string GetValue(string key, string defaultValue)
    {
      var keyvalue = KeyValues.FirstOrDefault(f => f.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase));
      if (keyvalue == null || string.IsNullOrEmpty(keyvalue.Value))
      {
        KeyValues.Add(new EsuKeyValue {Key = key, Value = defaultValue});
        Save();
        return defaultValue;
      }
      return keyvalue.Value;
    }

    public void Save()
    {
      var data = KeyValues.ToString().Encrypt();
      File.WriteAllText(fileName, data);
    }

    private void Load()
    {
      if (!File.Exists(fileName))
      {
        File.WriteAllText(fileName, "");
        keyValues = new EsuInfoCollection<EsuKeyValue>();
      }
      var data = File.ReadAllText(fileName);
      data = data.Decrypt();
      keyValues = data.Load<EsuInfoCollection<EsuKeyValue>>();
      if (keyValues == null)
      {
        keyValues = new EsuInfoCollection<EsuKeyValue>();
        if (keys != null)
        {
          foreach (var key in keys)
          {
            if (!keyValues.Any(a => a.Key.Equals(key, StringComparison.InvariantCultureIgnoreCase)))
            {
              keyValues.Add(new EsuKeyValue {Key = key});
            }
          }
        }
      }
    }
  }
}