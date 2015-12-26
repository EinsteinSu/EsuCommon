#region

using Supeng.Common.Entities;
using Supeng.Common.Entities.ObserveCollection;

#endregion

namespace Supeng.Common.Interfaces
{
  public interface IKeyValueStorage
  {
    EsuInfoCollection<EsuKeyValue> KeyValues { get; set; }

    string GetValue(string key, string defaultValue);

    void Save();
  }
}