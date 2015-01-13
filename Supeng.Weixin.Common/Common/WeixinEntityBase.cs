using Newtonsoft.Json;

namespace Supeng.Weixin.Common.Common
{
    public class WeixinEntityBase
    {
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
