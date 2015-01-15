using System.Collections.Generic;
using Supeng.Weixin.Common.Common;

namespace Supeng.Weixin.Common.User
{
    public class UserListResult : ResultBase
    {
        public IEnumerable<UserBase> userlist { get; set; }
    }
}
