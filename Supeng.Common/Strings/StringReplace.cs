using System.Collections.Generic;
using System.Linq;

namespace Supeng.Common.Strings
{
    public abstract class EsuStringReplace
    {
        protected abstract string Content { get; }

        protected abstract IEnumerable<ReplaceInfo> ReplaceList { get; }

        public virtual string Result()
        {
            return ReplaceList.Aggregate(Content, (current, info) => current.Replace(info.Target, info.Value));
        }
    }

    public class ReplaceInfo
    {
        public ReplaceInfo()
        {
            
        }

        public ReplaceInfo(string target, string value)
        {
            Target = target;
            Value = value;
        }

        public string Target { get; set; }

        public string Value { get; set; }
    }
}
