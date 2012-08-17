using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enums;

namespace Core.Caching
{
    public class CacheBase
    {
        /// <summary>
        /// 格式化 Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public string FormatKey(string key, MCaching.CacheGroup cacheGroup)
        {
            return string.Format("{0}.{1}", cacheGroup, key);
        }

        /// <summary>
        /// 校验 该key 是否属于该组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public bool VerifyKeyInGroup(string key, MCaching.CacheGroup cacheGroup)
        {
            var groupName = (key ?? "").Split('.')[0];
            return groupName.Equals(cacheGroup.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
