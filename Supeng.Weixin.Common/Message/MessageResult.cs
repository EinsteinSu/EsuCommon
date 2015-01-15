using Supeng.Weixin.Common.Common;

namespace Supeng.Weixin.Common.Message
{
    public class MessageResult : ResultBase
    {
        public string invaliduser { get; set; }

        public string invalidparty { get; set; }

        public string invalidtag { get; set; }
    }
}
