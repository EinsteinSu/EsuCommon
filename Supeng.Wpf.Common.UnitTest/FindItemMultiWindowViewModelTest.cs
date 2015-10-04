using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.Entities.ObserveCollection;
using Supeng.Wpf.Common.DialogWindows.ViewModels;

namespace Supeng.Wpf.Common.UnitTest
{
  [TestClass]
  public class FindItemMultiWindowViewModelTest
  {
    [TestMethod]
    public void Data_Selection()
    {
      var testViewModel = new TestViewModel();
      testViewModel.Load();
      testViewModel.Collection[0].Selected = true;
      Assert.IsTrue(testViewModel.SelectionDataCollection.Count == 1);
    }

    [TestMethod]
    public void Data_UnSelection()
    {
      var testViewModel = new TestViewModel();
      testViewModel.Load();
      Assert.IsTrue(testViewModel.SelectionDataCollection.Count == 0);
      Assert.IsFalse(string.IsNullOrEmpty(testViewModel.DataCheckMessage));
      Assert.AreEqual(testViewModel.DataCheckMessage, "请勾选一个选项！");
    }
  }

  internal class TestViewModel : FindItemMultiWindowViewModel<TestData>
  {
    protected override void Search()
    {
    }

    protected override EsuInfoCollection<TestData> GetCollection()
    {
      return TestData.GetTestDataCollection(10);
    }
  }
}