﻿using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Supeng.SmsMessage.Common.Test
{
    [TestClass]
    public class TestSendMessage
    {
        [TestMethod]
        public void TestMobileListConvert()
        {
            string expect = "1,2,3";
            var mobiles = new[] { "1", "2", "3" };
            var result = string.Join(",", mobiles);
            Assert.AreEqual(expect, result);
        }

        [TestMethod]
        public void TesSendMessageSuccess()
        {
            const string userName = "ynhrjs99";
            const string password = "123456";
            var messageSender = new MessageSender(userName, password);
            var result = messageSender.SendMessage("13825634085", "This is a message from server.【恒锐咨询】");
            Assert.AreEqual("成功", result);
        }
    }
}
