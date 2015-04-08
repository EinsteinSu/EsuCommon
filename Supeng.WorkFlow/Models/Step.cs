using System.Data;
using System.Data.SqlClient;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace Supeng.WorkFlow.Models
{
  public class Step : EsuInfoBase, IDataCreator<Step>, IDataSaveWithProcedure<Step>
  {
    private string workFlowID;
    private int stepOrderID;
    private string name;
    private string userID;
    private string dataModelName;
    private string dataModel;
    private string templateName;
    private string template;

    #region properties
    public string WorkFlowID
    {
      get { return workFlowID; }
      set
      {
        if (value.Equals(workFlowID)) return;
        workFlowID = value;
        NotifyOfPropertyChange(() => WorkFlowID);
      }
    }

    public int StepOrderID
    {
      get { return stepOrderID; }
      set
      {
        if (value.Equals(stepOrderID)) return;
        stepOrderID = value;
        NotifyOfPropertyChange(() => StepOrderID);
      }
    }

    public string Name
    {
      get { return name; }
      set
      {
        if (value.Equals(name)) return;
        name = value;
        NotifyOfPropertyChange(() => Name);
      }
    }

    public string UserID
    {
      get { return userID; }
      set
      {
        if (value.Equals(userID)) return;
        userID = value;
        NotifyOfPropertyChange(() => UserID);
      }
    }

    public string DataModelName
    {
      get { return dataModelName; }
      set
      {
        if (value.Equals(dataModelName)) return;
        dataModelName = value;
        NotifyOfPropertyChange(() => DataModelName);
      }
    }

    public string DataModel
    {
      get { return dataModel; }
      set
      {
        if (value.Equals(dataModel)) return;
        dataModel = value;
        NotifyOfPropertyChange(() => DataModel);
      }
    }

    public string TemplateName
    {
      get { return templateName; }
      set
      {
        if (value.Equals(templateName)) return;
        templateName = value;
        NotifyOfPropertyChange(() => TemplateName);
      }
    }

    public string Template
    {
      get { return template; }
      set
      {
        if (value.Equals(template)) return;
        template = value;
        NotifyOfPropertyChange(() => Template);
      }
    }

    #endregion

    #region Implement IDataCreate
    public Step CreateData(IDataReader reader)
    {
      var data = new Step();
      data.ID = reader["id"].ToString();
      data.WorkFlowID = reader["workflowid"].ToString();
      data.StepOrderID = reader["steporderid"].ToString().ConvertData(0);
      data.Name = reader["name"].ToString();
      data.Description = reader["description"].ToString();
      data.UserID = reader["userid"].ToString();
      data.DataModelName = reader["datamodelname"].ToString();
      data.DataModel = reader["datamodel"].ToString();
      data.TemplateName = reader["templatename"].ToString();
      data.Template = reader["template"].ToString();
      return data;
    }
    #endregion

    #region Implement IDataSavewithProcedure
    public IDataParameter[] MappingParameters(Step data)
    {
      var parameters = new IDataParameter[10];
      parameters[0] = new SqlParameter("@ID", data.ID);
      parameters[1] = new SqlParameter("@WorkFlowID", data.WorkFlowID);
      parameters[2] = new SqlParameter("@StepOrderID", data.StepOrderID);
      parameters[3] = new SqlParameter("@Name", data.Name);
      parameters[4] = new SqlParameter("@Description", data.Description);
      parameters[5] = new SqlParameter("@UserID", data.UserID);
      parameters[6] = new SqlParameter("@DataModelName", data.DataModelName);
      parameters[7] = new SqlParameter("@DataModel", data.DataModel);
      parameters[8] = new SqlParameter("@TemplateName", data.TemplateName);
      parameters[9] = new SqlParameter("@Template", data.Template);
      return parameters;
    }

    public string GetProcedureName()
    {
      return "[dbo].[StepInsert]";
    }
    #endregion
  }
}
