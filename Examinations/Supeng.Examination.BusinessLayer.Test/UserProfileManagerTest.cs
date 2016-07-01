using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Supeng.Examination.Model;

namespace Supeng.Examination.BusinessLayer.Test
{
    [TestClass]
    public class UserProfileManagerTest : TestBase
    {
        private const string Password = "123$$$";
        private readonly UserProfileManager _manager = new UserProfileManager();

        [TestMethod]
        public void Select()
        {
            Add();
            Assert.IsTrue(_manager.SelectList().Any());
        }

        [TestMethod]
        public void Create()
        {
            var user = Add();
            Assert.IsTrue(user.UserId > 0);
        }

        [TestMethod]
        public void Modify()
        {
            const string name = "Name1";
            var user = Add();
            user.Name = name;
            _manager.Modify(user);
            user = Context.UserProfiles.FirstOrDefault(f => f.UserId == user.UserId);
            Assert.IsNotNull(user);
            Assert.AreEqual(user.Name, name);
        }

        [TestMethod]
        public void Delete()
        {
            var user = Add();
            _manager.Delete(user.UserId);
            user = Context.UserProfiles.FirstOrDefault(f => f.UserId == user.UserId);
            Assert.IsNull(user);
        }

        [TestMethod]
        public void Logon_Success()
        {
            var user = Add();
            var result = _manager.Logon(user.Name, Password);
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void Logon_Failed()
        {
            var user = Add();
            var result = _manager.Logon(user.Name, Password + "$");
            Assert.IsNull(result);
        }

        [TestMethod]
        public void ChangePassword_Success()
        {
            const string newPassword = Password + "$";
            var user = Add();
            var error = _manager.ChangePassword(user.Name, Password, newPassword);
            Assert.IsTrue(string.IsNullOrEmpty(error));
        }

        [TestMethod]
        public void ChangePassword_Validate_Failed()
        {
            const string newPassword = Password + "$";
            var user = Add();
            var error = _manager.ChangePassword(user.Name, newPassword, newPassword);
            Assert.IsFalse(string.IsNullOrEmpty(error));
        }

        protected UserProfile Add()
        {
            var profile = new UserProfile
            {
                Name = "test",
                UserCode = "123456",
                Password = Password
            };
            var error = _manager.Create(profile);
            Assert.IsTrue(string.IsNullOrEmpty(error));
            return profile;
        }
    }
}