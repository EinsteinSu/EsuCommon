using System.Collections.Generic;
using Supeng.Weixin.Common.Common;

namespace Supeng.Weixin.Common.User
{
    public class UserDetail : UserBase
    {
        public UserDetail()
        {
            extatrr = new UserAttribute();
            enable = 1;
        }

        public IEnumerable<int> department { get; set; }

        public string position { get; set; }

        public string mobile { get; set; }

        public string email { get; set; }

        public string weixinid { get; set; }

        public int enable { get; set; }

        public UserAttribute extatrr { get; set; }
    }

    public class UserAttribute
    {
        public UserAttribute()
        {
            attrs = new WeixinAttribute[0];
        }

        public UserAttribute(IEnumerable<WeixinAttribute> attributes)
        {
            attrs = attributes;
        }

        public IEnumerable<WeixinAttribute> attrs { get; set; }
    }
}
