using System;
using System.ComponentModel.DataAnnotations;

namespace Supeng.Silverlight.Common.Entities.BasesEntities
{
  public class TreeEntityBase : EsuInfoBase
  {
    private string pid;

    [Display(AutoGenerateField = false)]
    public string PID
    {
      get { return pid; }
      set
      {
        if (value == pid) return;
        pid = value;
        NotifyOfPropertyChange(() => PID);
      }
    }

    public virtual void InitalizeParentData()
    {
      ID = Guid.NewGuid().ToString();
      PID = "0";
    }

    public virtual void InitalizeChildData(TreeEntityBase parent)
    {
      InitalizeParentData();
      PID = parent.ID;
    }
  }
}