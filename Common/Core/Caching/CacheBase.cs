using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enums;
using Core.ConfigUtility;

namespace Core.Caching
{
    /// <summary>
    /// 缓存基类
    /// </summary>
    public class CacheBase
    {
        /// <summary>
        /// 格式化 Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public static string FormatKey(string key, MCaching.CacheGroup cacheGroup)
        {
            //var versionId = MConfigManager.GetAppSettingsValue<string>(
            //    MConfigManager.FormatKey(cacheGroup + "_Version", MConfigs.ConfigsCategory.Cache),"1.0");
            var versionId = DateTime.Now.ToString("yyyyMMddHHmmssffffff");
            if (cacheGroup != MCaching.CacheGroup.Cache)
            {
                var cache = MCacheManager.GetCacheObj();
                var cacheKey = cacheGroup + "_Version";
                if (cache.Contains(cacheKey, MCaching.CacheGroup.Cache))
                    versionId = cache.GetValByKey<string>(cacheKey, MCaching.CacheGroup.Cache);
                else
                    cache.Set(cacheKey, MCaching.CacheGroup.Cache, versionId);
            }
            else
            {
                versionId = "0";
            }
            return string.Format("{0}.{1}.{2}", cacheGroup, versionId, key);
        }

        /// <summary>
        /// 校验 该key 是否属于该组
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public static bool VerifyKeyInGroup(string key, MCaching.CacheGroup cacheGroup)
        {
            var groupName = (key ?? "").Split('.')[0];
            return groupName.Equals(cacheGroup.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
