namespace Supeng.Weixin.Common.Message
{
    public class MessageResult
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }

        public string invaliduser { get; set; }

        public string invalidparty { get; set; }

        public string invalidtag { get; set; }
    }
}
