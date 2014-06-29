using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Xml.Serialization;
using Caliburn.Micro;
using Newtonsoft.Json;

namespace Supeng.Common.Entities
{
  public class EsuInfoBase : PropertyChangedBase, ICloneable
  {
    private string description;
    private string id;

    public EsuInfoBase()
    {
    }

    public EsuInfoBase(bool initalizeID)
    {
      if (initalizeID)
        id = Guid.NewGuid().ToString();
    }

    #region properties

    [Display(AutoGenerateField = false)]
    public string ID
    {
      get { return id; }
      set
      {
        if (value == id) return;
        id = value;
        NotifyOfPropertyChange(() => ID);
      }
    }

    [Display(AutoGenerateField = false)]
    public string Description
    {
      get { return description; }
      set
      {
        if (value == description) return;
        description = value;
        NotifyOfPropertyChange(() => Description);
      }
    }

    [Display(AutoGenerateField = false)]
    public new bool IsNotifying
    {
      get { return base.IsNotifying; }
      set { base.IsNotifying = value; }
    }

    #endregion

    public object Clone()
    {
      return JsonConvert.DeserializeObject(GetSerializeString(), GetType());
    }

    #region serialize

    public string GetSerializeString()
    {
      return JsonConvert.SerializeObject(this);
    }

    public void SerializeToXml(string fileName)
    {
      using (var writer = new StreamWriter(fileName))
      {
        var xs = new XmlSerializer(GetType());
        xs.Serialize(writer, this);
      }
    }

    public void SerializeToText(string fileName)
    {
      File.WriteAllText(fileName, ToString());
    }

    #endregion

    public override string ToString()
    {
      return GetSerializeString();
    }

    public static T InitailizeDefaultData<T>() where T : EsuInfoBase, new()
    {
      var data = new T {ID = Guid.NewGuid().ToString()};
      return data;
    }
  }
}