using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Http.Common;

namespace Supeng.Weixin.Test
{
    [TestClass]
    public class ConnectTest
    {
        [TestMethod]
        public void Connect()
        {
            var weixin = new Common.Weixin("wx22e0f747c0c25ab6",
                "DMLT3v8_c2dUK3KOKnBkSRixC3DrW67wS7HtOf20IHHWcK8ZESjg4EKCJPofkjde");
            Console.WriteLine(weixin.Token.access_token);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(weixin.Token.access_token));
        }
    }
}
