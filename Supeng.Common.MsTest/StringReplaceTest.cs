using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Common.Strings;

namespace Supeng.Common.MsTest
{
    [TestClass]
    public class StringReplaceTest
    {
        [TestMethod]
        public void TestReplace()
        {
            Assert.AreEqual(new TestStringReplace().Result(), "Hello Einstein!Hello");
        }
    }

    internal class TestStringReplace : EsuStringReplace
    {
        protected override string Content
        {
            get { return "Hello @user!@hello"; }
        }

        protected override IEnumerable<ReplaceInfo> ReplaceList
        {
            get { return new[] { new ReplaceInfo("@user", "Einstein"), new ReplaceInfo("@hello", "Hello") }; }
        }
    }
}
