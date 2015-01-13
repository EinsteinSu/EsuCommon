using System;
using Newtonsoft.Json;
using Supeng.Http.Common;
using Supeng.Weixin.Common.Message;

namespace Supeng.Weixin.Common
{
    public class Weixin
    {
        private readonly string corpId;
        private readonly string secretId;
        private AccessToken accessToken;

        public Weixin(string corpId, string secretId)
        {
            this.corpId = corpId;
            this.secretId = secretId;
        }


        public AccessToken Token
        {
            get
            {
                if (accessToken == null)
                {
                    string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", corpId, secretId);
                    using (var client = new EsuWebClient())
                    {
                        accessToken = client.GetData<AccessToken>(url);
                    }
                }
                return accessToken;
            }
        }

        public string Post(string url, string data)
        {
            using (var client = new EsuWebClient())
            {
                return client.Post(url, data);
            }
        }

        public MessageResult SendMessage(MessageBase message)
        {
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}",
                Token.access_token);
            return JsonConvert.DeserializeObject<MessageResult>(Post(url, message.ToString()));
        }

        public bool SendMessageWithResult(MessageBase message)
        {
            var result = SendMessage(message);
            return result.errmsg.Equals("ok", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
