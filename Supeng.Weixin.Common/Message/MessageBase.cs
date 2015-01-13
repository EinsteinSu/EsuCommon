using Supeng.Weixin.Common.Common;

namespace Supeng.Weixin.Common.Message
{
    public class MessageBase : WeixinEntityBase
    {
        public MessageBase()
        {
            touser = "";
            toparty = "";
            totag = "";
            agentid = 1;
        }

        public string touser { get; set; }

        public string toparty { get; set; }

        public string totag { get; set; }

        public string msgtype { get; set; }

        public int agentid { get; set; }

        public int safe { get; set; }
    }
}
