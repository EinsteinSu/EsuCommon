using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Common.Entities
{
  public class EsuKeyValue : EsuInfoBase
  {
    private string key;
    private string value;

    public string Key
    {
      get { return key; }
      set
      {
        if (value == key) return;
        key = value;
        NotifyOfPropertyChange(() => Key);
      }
    }

    public string Value
    {
      get { return value; }
      set
      {
        if (value == this.value) return;
        this.value = value;
        NotifyOfPropertyChange(() => Value);
      }
    }
  }
}
