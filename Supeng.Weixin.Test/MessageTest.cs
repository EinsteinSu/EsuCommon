using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Weixin.Common;
using Supeng.Weixin.Common.Message;
using Supeng.Weixin.Common.Utility;

namespace Supeng.Weixin.Test
{
    [TestClass]
    public class MessageTest
    {
        [TestMethod]
        public void TestMessageContent()
        {
            var message = new TextMessage("短信【恒锐咨询】");
            message.touser = "76507593";
            Console.WriteLine(message);
        }

        [TestMethod]
        public void TestSendTextMessage()
        {
            var weixin = new Common.Weixin("wx22e0f747c0c25ab6",
                "DMLT3v8_c2dUK3KOKnBkSRixC3DrW67wS7HtOf20IHHWcK8ZESjg4EKCJPofkjde");
            var result = weixin.SendTextMessage(new TextMessage("恒锐消防咨询测试微信内容") { touser = "76507593" });
            Console.WriteLine(result);
            Assert.IsTrue(result.errmsg.Equals("ok", StringComparison.InvariantCultureIgnoreCase));
        }

        [TestMethod]
        public void TestNewsMessageContent()
        {
            var weixin = new Common.Weixin("wx22e0f747c0c25ab6",
               "DMLT3v8_c2dUK3KOKnBkSRixC3DrW67wS7HtOf20IHHWcK8ZESjg4EKCJPofkjde");
            var message = new NewsMessage();
            message.news = new NewsCollection(new[] { new News { title = "test", description = "description", url = "http://hiever.net:1314" } });
            var result = weixin.SendTextMessage(message);
            Console.WriteLine(result);
        }
    }
}
