using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Supeng.Weixin.Common
{
    public class AccessToken
    {
        public string access_token { get; set; }

        public long expires_in { get; set; }
    }
}
