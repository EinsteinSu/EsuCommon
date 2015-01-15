using Newtonsoft.Json;
using Supeng.Weixin.Common.Common;

namespace Supeng.Weixin.Common.User
{
    public class UserBase : WeixinEntityBase
    {
        public string userid { get; set; }

        public string name { get; set; }
    }
}