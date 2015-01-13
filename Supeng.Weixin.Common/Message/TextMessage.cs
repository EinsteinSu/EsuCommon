namespace Supeng.Weixin.Common.Message
{
    public class TextMessage : MessageBase
    {
        public TextMessage(string textContent)
        {
            msgtype = "text";
            text = new TextMessageContent { content = textContent };
        }

        public TextMessageContent text { get; set; }
    }

    public class TextMessageContent
    {
        public string content { get; set; }
    }
}
