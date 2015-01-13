using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Weixin.Common;
using Supeng.Weixin.Common.Message;

namespace Supeng.Weixin.Test
{
    [TestClass]
    public class TextMessageTest
    {
        [TestMethod]
        public void TestMessageContent()
        {
            var message = new TextMessage("This is a test message");
            message.touser = "76507593";
            Console.WriteLine(message);
        }

        [TestMethod]
        public void TestSendMessage()
        {
            var weixin = new Common.Weixin("wx22e0f747c0c25ab6",
                "DMLT3v8_c2dUK3KOKnBkSRixC3DrW67wS7HtOf20IHHWcK8ZESjg4EKCJPofkjde");
            var result = weixin.SendMessage(new TextMessage("This is a test message") { touser = "76507593" });
            Console.WriteLine(result);
            Assert.IsTrue(result.errmsg.Equals("ok", StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
