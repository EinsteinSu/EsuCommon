using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Weixin.Common.Common;
using Supeng.Weixin.Common.User;
using Supeng.Weixin.Common.Utility;

namespace Supeng.Weixin.Test
{
    [TestClass]
    public class WeixinContactTest
    {
        private Common.Weixin weixin;
        [TestInitialize]
        public void Init()
        {
            weixin = new Common.Weixin("wx22e0f747c0c25ab6",
              "DMLT3v8_c2dUK3KOKnBkSRixC3DrW67wS7HtOf20IHHWcK8ZESjg4EKCJPofkjde");
        }

        [TestMethod]
        public void TestGetAllUsers()
        {
            var result = weixin.GetUserList(1, 1);
            Console.WriteLine(result);
            Assert.IsTrue(result.userlist.Any());
        }

        [TestMethod]
        public void TestWeixinUpdates()
        {
            var user = new UserDetail();
            user.userid = "76507593";
            user.name = "Einstein Su";
            user.department = new List<int> { 1, 2 };
            user.mobile = "13825634085";
            user.email = "einstein_supeng@hotmail.com";
            user.enable = 1;
            user.extatrr = new UserAttribute(new[] { new WeixinAttribute { name = "爱好", value = "旅游" } });
            var result = weixin.SaveUser(user, OperateType.Update);
            Console.WriteLine(result);
            Assert.IsTrue(result.Success("updated"));
        }
    }
}
