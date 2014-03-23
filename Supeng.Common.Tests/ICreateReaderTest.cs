using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
