using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.DataOperations;
using Supeng.Common.Entities;
using Supeng.Common.IOs;

namespace Supeng.Common.MsTest
{
  [TestClass]
  public class FileKeyValueStorageTest
  {
    private const string FileName = "C:\\test.config";

    [TestCleanup]
    public void Clean()
    {
      try
      {
        if (File.Exists(FileName))
          File.Delete(FileName);
      }
      catch (Exception ex)
      {
        Console.WriteLine("Delete file error.{0}", ex.Message);
      }
    }

    [TestMethod]
    public void Save()
    {
      var storage = new FileKeyValueStorage(FileName);
      var collection = storage.KeyValues;
      Assert.IsTrue(collection.Count == 0);
      collection.Add(new EsuKeyValue { Key = "Test", Value = "Test1" });
      storage.Save();
      storage = new FileKeyValueStorage(FileName);
      var result = storage.GetValue("Test", "");
      Assert.AreEqual("Test1", result);
    }

    [TestMethod]
    public void Load()
    {
      var storage = new FileKeyValueStorage(FileName, new[] { "A", "B", "C" });
      Assert.IsTrue(storage.KeyValues.Count == 3);
    }
  }
}
