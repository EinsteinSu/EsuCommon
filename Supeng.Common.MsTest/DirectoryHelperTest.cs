using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.IOs;

namespace Supeng.Common.MsTest
{
  [TestClass]
  public class DirectoryHelperTest
  {
    [TestMethod]
    public void CurrentDirectory()
    {
      Assert.AreEqual(DirectoryHelper.CurrentDirectory, Environment.CurrentDirectory);
    }
  }
}
