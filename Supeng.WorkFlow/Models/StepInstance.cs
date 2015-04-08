using System;
using System.Data;
using System.Data.SqlClient;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.Types;

namespace Supeng.WorkFlow.Models
{
  public class StepInstance : EsuInfoBase, IDataCreator<StepInstance>, IDataSaveWithProcedure<StepInstance>
  {
    private string accept;
    private string processData;
    private string processDescription;
    private DateTime processTime;
    private bool processed;
    private string reject;
    private string serialNumber;
    private string stepID;
    private string userID;

    #region properties

    public string StepID
    {
      get { return stepID; }
      set
      {
        if (value.Equals(stepID)) return;
        stepID = value;
        NotifyOfPropertyChange(() => StepID);
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

    public string SerialNumber
    {
      get { return serialNumber; }
      set
      {
        if (value.Equals(serialNumber)) return;
        serialNumber = value;
        NotifyOfPropertyChange(() => SerialNumber);
      }
    }

    public string Accept
    {
      get { return accept; }
      set
      {
        if (value.Equals(accept)) return;
        accept = value;
        NotifyOfPropertyChange(() => Accept);
      }
    }

    public string Reject
    {
      get { return reject; }
      set
      {
        if (value.Equals(reject)) return;
        reject = value;
        NotifyOfPropertyChange(() => Reject);
      }
    }

    public DateTime ProcessTime
    {
      get { return processTime; }
      set
      {
        if (value.Equals(processTime)) return;
        processTime = value;
        NotifyOfPropertyChange(() => ProcessTime);
      }
    }

    public string ProcessData
    {
      get { return processData; }
      set
      {
        if (value.Equals(processData)) return;
        processData = value;
        NotifyOfPropertyChange(() => ProcessData);
      }
    }

    public string ProcessDescription
    {
      get { return processDescription; }
      set
      {
        if (value.Equals(processDescription)) return;
        processDescription = value;
        NotifyOfPropertyChange(() => ProcessDescription);
      }
    }

    public bool Processed
    {
      get { return processed; }
      set
      {
        if (value.Equals(processed)) return;
        processed = value;
        NotifyOfPropertyChange(() => Processed);
      }
    }

    #endregion

    public StepInstance CreateData(IDataReader reader)
    {
      var data = new StepInstance
      {
        ID = reader["id"].ToString(),
        StepID = reader["stepid"].ToString(),
        UserID = reader["userid"].ToString(),
        SerialNumber = reader["serialnumber"].ToString(),
        Accept = reader["accept"].ToString(),
        Reject = reader["reject"].ToString(),
        ProcessTime = reader["processtime"].ToString().ConvertToDateTime(),
        ProcessData = reader["processdata"].ToString(),
        ProcessDescription = reader["processdescription"].ToString(),
        Processed = reader["processed"].ToString().ConvertData<bool>()
      };
      return data;
    }

    public IDataParameter[] MappingParameters(StepInstance data)
    {
      var parameters = new IDataParameter[10];
      parameters[0] = new SqlParameter("@ID", data.ID);
      parameters[1] = new SqlParameter("@StepID", data.StepID);
      parameters[2] = new SqlParameter("@UserID", data.UserID);
      parameters[3] = new SqlParameter("@SerialNumber", data.SerialNumber);
      parameters[4] = new SqlParameter("@Accept", data.Accept);
      parameters[5] = new SqlParameter("@Reject", data.Reject);
      parameters[6] = new SqlParameter("@ProcessTime", data.ProcessTime);
      parameters[7] = new SqlParameter("@ProcessData", data.ProcessData);
      parameters[8] = new SqlParameter("@ProcessDescription", data.ProcessDescription);
      parameters[9] = new SqlParameter("@Processed", data.Processed);
      return parameters;
    }

    public string GetProcedureName()
    {
      return "dbo.StepInstanceUpdate";
    }
  }
}