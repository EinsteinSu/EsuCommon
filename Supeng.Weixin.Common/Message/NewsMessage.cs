using System.Collections.Generic;

namespace Supeng.Weixin.Common.Message
{
    public class NewsMessage : MessageBase
    {
        public NewsMessage()
        {
            msgtype = "news";
        }

        public NewsCollection news { get; set; }
    }

    public class NewsCollection
    {
        public NewsCollection() { }

        public NewsCollection(IEnumerable<News> news)
        {
            articles = news;
        }

        public IEnumerable<News> articles { get; set; }
    }

    public class News
    {
        public string title { get; set; }

        public string description { get; set; }

        public string url { get; set; }

        public string picurl { get; set; }
    }
}
