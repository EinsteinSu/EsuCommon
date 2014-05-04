using System.Collections.Generic;
using System.Linq;
using DevExpress.Xpf.Printing;
using Supeng.Common.Entities;
using Supeng.Common.Interfaces;

namespace Supeng.Wpf.Common.DialogWindows.ViewModels
{
  public abstract class ReportWindowBase : EsuInfoBase, IDataLoad
  {
    public abstract string Title { get; }

    protected abstract string ServiceUrl { get; }

    protected abstract string ReportName { get; }

    protected virtual Dictionary<string, object> Parameters
    {
      get { return null; }
    }

    public virtual ReportPreviewModel Model
    {
      get
      {
        var model = new ReportPreviewModel(ServiceUrl) { ReportName = ReportName, IsParametersPanelVisible = false };
        if (Parameters != null && Parameters.Any())
        {
          foreach (var parameter in Parameters)
            model.Parameters[parameter.Key].Value = parameter.Value;
        }
        model.CreateDocument();
        return model;
      }
    }

    public virtual void Load()
    {
      //Model.CreateDocument();
    }
  }
}