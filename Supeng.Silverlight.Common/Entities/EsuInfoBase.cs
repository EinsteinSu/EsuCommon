using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Windows;
using Caliburn.Micro;
using Newtonsoft.Json;
using Supeng.Silverlight.Common.IOs;
using Action = System.Action;

namespace Supeng.Silverlight.Common.Entities
{
  public class EsuInfoBase : PropertyChangedBase
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

    public override string ToString()
    {
      return GetSerializeString();
    }

    public object Clone()
    {
      return JsonConvert.DeserializeObject(GetSerializeString(), GetType());
    }

    public static T InitailizeDefaultData<T>() where T : EsuInfoBase, new()
    {
      var data = new T { ID = Guid.NewGuid().ToString() };
      return data;
    }

    #region serialize

    public string GetSerializeString()
    {
      return JsonConvert.SerializeObject(this);
    }

    public void SerializeToText(string fileName)
    {
      StreamHelper.WriteText(fileName, GetSerializeString());
    }

    #endregion

    protected void EsuNotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> propertyChange)
    {
      if (propertyChange != null)
      {
        if (Deployment.Current.CheckAccess())
        {
          NotifyOfPropertyChange(propertyChange);
        }
        else
        {
          Deployment.Current.Dispatcher.BeginInvoke(() =>
          {
            NotifyOfPropertyChange(propertyChange);
          });
        }
      }
    }
  }
}