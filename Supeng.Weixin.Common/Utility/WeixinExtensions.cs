using System;
using Newtonsoft.Json;
using Supeng.Weixin.Common.Common;
using Supeng.Weixin.Common.Message;
using Supeng.Weixin.Common.Properties;
using Supeng.Weixin.Common.User;

namespace Supeng.Weixin.Common.Utility
{
    public static class WeixinExtensions
    {
        public static MessageResult SendTextMessage(this Weixin weixin, MessageBase message)
        {
            string url = string.Format(Resources.SendMessageUrl, weixin.Token.access_token);
            return JsonConvert.DeserializeObject<MessageResult>(weixin.Post(url, message.ToString()));
        }

        public static UserListResult GetUserList(this Weixin weixin, int departmentid, int fetch = 0, UserStatus status = UserStatus.全部)
        {
            string url = string.Format(Resources.GetUserListUrl, weixin.Token.access_token, departmentid, fetch, (int)status);
            return weixin.Get<UserListResult>(url);
        }

        public static ResultBase SaveUser(this Weixin weixin, UserDetail user, OperateType operate = OperateType.Create)
        {
            string source = string.Empty;
            switch (operate)
            {
                case OperateType.Create:
                    source = Resources.CreateUserUrl;
                    break;
                case OperateType.Update:
                    source = Resources.UpdateUserUrl;
                    break;
                case OperateType.Delete:
                    source = Resources.DeleteUserUrl;
                    break;
            }
            string url = operate == OperateType.Delete ? string.Format(source, weixin.Token.access_token, user.userid) : string.Format(source, weixin.Token.access_token);
            if (operate == OperateType.Delete)
                return weixin.Get<ResultBase>(url);
            return weixin.Post<ResultBase>(url, user.ToString());
        }

        public static bool Success(this ResultBase result, string flag)
        {
            return result.errmsg.Equals(flag, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
