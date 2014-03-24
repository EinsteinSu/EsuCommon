using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Supeng.Common.Entities.BasesEntities.DataEntities;
using Supeng.Common.Exceptions;
using Supeng.Common.Threads;
using Supeng.Data.Sql;

namespace TestInWindows
{
  public partial class Form1 : Form
  {
    private readonly SqlDataStorage<ParameterInfo<string>> storage;
    public Form1()
    {
      InitializeComponent();
      TaskScheduler scheduler = TaskScheduler.FromCurrentSynchronizationContext();
      storage =
       new SqlDataStorage<ParameterInfo<string>>(
         "Data Source=116.54.125.122;Initial Catalog=DataTests;Persist Security Info=True;User ID=sa;Password=hrmaster");
      cancellationTokenSource = storage.Cancellation;
    }

    private void button1_Click(object sender, EventArgs e)
    {
      string guid = Guid.NewGuid().ToString();
      string sql = string.Format("Insert into parameter(id) values('{0}')", guid);
      storage.ExecuteInBackground(sql, new TestInWindowsBackgroudData(1, "Insert Thread", richTextBox1));
      sql = string.Format("Delete from parameter where id = '{0}'", guid);
      storage.ExecuteInBackground(sql, new TestInWindowsBackgroudData(1, "Delete Thread", richTextBox1));
    }

    private CancellationTokenSource cancellationTokenSource;
    private void button2_Click(object sender, EventArgs e)
    {
      string guid = Guid.NewGuid().ToString();
      string sql = string.Format("Insert into parameter(id) values('{0}')", guid);
      
      storage.ExecuteWithAPM(sql, new TestInWindowsBackgroudData(1, "Insert Thread", richTextBox1));
      
      sql = string.Format("Delete from parameter where id = '{0}'", guid);
      storage.ExecuteWithAPM(sql, new TestInWindowsBackgroudData(1, "Delete Thread", richTextBox1));
    }

    private void button3_Click(object sender, EventArgs e)
    {
      cancellationTokenSource.Cancel();
    }
  }

  public class TestInWindowsBackgroudData : IBackgroundData<int>, IExceptionHandle
  {
    private readonly int assertResult;
    private readonly string head;
    private readonly RichTextBox outputControl;

    public TestInWindowsBackgroudData(int assertResult, string head, RichTextBox outputControl)
    {
      this.assertResult = assertResult;
      this.head = head;
      this.outputControl = outputControl;
    }

    public void BeginExecute()
    {
      AppendText(@"Begin execute");

    }

    public void EndExecute(int result)
    {
      if (result - assertResult != 0)
        AppendText(@"Error result.");
      else
        AppendText(string.Format("Execute success the effect result has {0} records.", result));
    }

    public void CancelExecute()
    {
      AppendText(@"Execute has canceled.");
    }

    public void HandleBackgroundException(Exception[] exceptions)
    {
      foreach (Exception exception in exceptions)
      {
        AppendText(exception.Message + Environment.NewLine);
      }
    }

    public void Handle(Exception ex)
    {
      AppendText(ex.Message);
    }

    protected virtual void AppendText(string text)
    {
      outputControl.AppendText(string.Format("[{0}]:{1} [{2}]{3}", head, text, DateTime.Now, Environment.NewLine));
    }
  }
}