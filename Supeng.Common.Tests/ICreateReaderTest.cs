using System.Data;
using Supeng.Common.DataOperations;

namespace Supeng.Common.Tests
{
  public class CreateReaderTest
  {
  }

  internal sealed class CreateReader : IDataCreator<string>
  {
    public string CreateData(IDataReader reader)
    {
      return reader["test"].ToString();
    }
  }
}