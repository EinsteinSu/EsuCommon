using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Supeng.Data.Sql.MsTest
{
  public class DataTestBase
  {
    protected string Connection;
    protected SqlDataStorage Storage;
    public DataTestBase()
    {
      var builder = new SqlConnectionStringBuilder
      {
        InitialCatalog = "UnitTest",
        DataSource = "116.54.125.122",
        UserID = "sa",
        Password = "hrmaster"
      };
      Connection = builder.ConnectionString;
      Storage = new SqlDataStorage(Connection, TaskScheduler.Current);
    }
  }
}
