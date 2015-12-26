using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Win32;
using Supeng.Common.IOs;

namespace Supeng.Common.MsTest
{
  [TestClass]
  public class EsuRegistryTest
  {
    [TestMethod]
    public void TestSubNames()
    {
      Registry.LocalMachine.SetValue("EsuTest", "Test");
      var data = Registry.LocalMachine.GetValue("EsuTest");
      Assert.AreEqual(data, "Test");

      data = Registry.LocalMachine.GetValue("EsuTest1", "Test1");
      Assert.AreEqual(data, "Test1");
      Registry.LocalMachine.SetValue("EsuTest1", "Test");
      data = Registry.LocalMachine.GetValue("EsuTest1", "Test1");
      Assert.AreEqual(data, "Test");
    }

    [TestMethod]
    public void GetRegist()
    {
      string key = "Esu";
      string data = "Test";
      var registry = new EsuRegistry(new[] { key });
      registry.Registries[0].Description = data;
      registry.Save();
      var value = EsuRegistryExtensions.GetDataFromLocalMachine(key, "Test1");
      Assert.AreEqual(data, value);
    }
  }
}
