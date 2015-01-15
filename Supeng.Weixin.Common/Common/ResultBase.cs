using Newtonsoft.Json;

namespace Supeng.Weixin.Common.Common
{
    public class ResultBase : WeixinEntityBase
    {
        public int errcode { get; set; }

        public string errmsg { get; set; }
    }
}