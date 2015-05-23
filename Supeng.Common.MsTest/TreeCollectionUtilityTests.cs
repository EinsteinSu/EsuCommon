using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.Entities.BasesEntities;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Common.Entities.Utilitis;
using Supeng.Common.Interfaces;

namespace Supeng.Common.MsTest
{
  [TestClass]
  public class TreeCollectionUtilityTests
  {
    [TestMethod]
    public void TestCheck()
    {
      var collection = new EsuInfoCollection<TreeData>();
      collection.Add(new TreeData("1", "0"));
      collection.Add(new TreeData("2", "1"));
      collection.Add(new TreeData("3", "1"));
      collection.Add(new TreeData("4", "0"));
      collection.Add(new TreeData("5", "4"));
      var item = collection[0];
      item.IsChecked = true;
      item.Check(collection);
      Assert.IsTrue(collection[1].IsChecked);
      Assert.IsTrue(collection[2].IsChecked);
      Assert.IsFalse(collection[3].IsChecked);
      Assert.IsFalse(collection[4].IsChecked);
      Console.WriteLine(collection.ToString());
      item.IsChecked = false;
      item.Check(collection);
      Assert.IsFalse(collection[1].IsChecked);
      Assert.IsFalse(collection[2].IsChecked);
      Assert.IsFalse(collection[3].IsChecked);
      Assert.IsFalse(collection[4].IsChecked);
      Console.WriteLine(collection.ToString());

    }
  }

  public class TreeData : TreeEntityBase, IChecked
  {
    public TreeData(string id, string pid)
    {
      ID = id;
      PID = pid;
    }

    public bool IsChecked { get; set; }

    public string Name
    {
      get { return ID + "-" + PID; }
    }
  }
}
