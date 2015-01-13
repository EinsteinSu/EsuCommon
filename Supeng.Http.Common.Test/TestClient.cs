using System;
using System.Linq;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Nito.AsyncEx.UnitTests;

namespace Supeng.Http.Common.Test
{
    [TestClass]
    public class TestClient
    {
        [TestMethod]
        public void TestGetString()
        {
            var url = "http://en.wikipedia.org/";
            var client = new EsuWebClient();
            var result = client.GetString(url);
            Console.WriteLine(result);
            Assert.IsTrue(result.Length > 0);
        }

    }
}
