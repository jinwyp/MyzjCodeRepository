using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Caching.Memcached;
using Core.Caching.Redis;
using Core.ConfigUtility;
using Core.Enums;
using ServiceStack.CacheAccess;
using Core.LogUtility;

namespace Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public static class MCacheManager
    {

        private static ICache _cacheObj = null;

        /// <summary>
        /// 获取缓存对象 自定义缓存类型
        /// </summary>
        /// <returns></returns>
        public static ICache GetCacheObj(MCaching.Provider provider)
        {
            if (_cacheObj == null)
            {
                switch (provider)
                {
                    case MCaching.Provider.Redis:
                        {
                            _cacheObj = MRedisCache.GetInstance();
                        }
                        break;
                    case MCaching.Provider.Memcached:
                        {
                            _cacheObj = MMemcache.GetInstance();
                        }
                        break;
                    default:
                        {
                            MLogManager.Error(MLogGroup.Other.获取缓存对象, "", "缓存方式错误 没有指定 " + MCaching.Provider.Memcached + " 缓存的实现方法！");
                        }
                        break;
                }
            }
            return _cacheObj;
        }

        /// <summary>
        /// 获取缓存对象 采用 配置文件配置
        /// </summary>
        /// <returns></returns>
        public static ICache GetCacheObj()
        {
            if (_cacheObj == null)
            {
                var key = MConfigManager.FormatKey("Default", MConfigs.ConfigsCategory.Cache);
                var cacheType = MConfigManager.GetAppSettingsValue(key, "");
                if (!string.IsNullOrEmpty(cacheType))
                {
                    if (cacheType.Equals(MCaching.Provider.Redis.ToString(), StringComparison.OrdinalIgnoreCase))
                        _cacheObj = MRedisCache.GetInstance();
                    else if (cacheType.Equals(MCaching.Provider.Memcached.ToString(), StringComparison.OrdinalIgnoreCase))
                        _cacheObj = MMemcache.GetInstance();
                    else
                        MLogManager.Error(MLogGroup.Other.获取缓存对象, "", "缓存方式配置无法识别，节点" + key);
                }
                else
                {
                    MLogManager.Error(MLogGroup.Other.获取缓存对象, "", "请配置缓存方式，节点" + key);
                }
            }
            return _cacheObj;
        }


        public static T UseCached<T>(string key, MCaching.CacheGroup cacheGroup, ICache cache, Delegate func) where T : class
        {
            #region 使用缓存

            var cacheResult = cache.GetValByKey<T>(key, cacheGroup);
            if (cacheResult == null)
            {
                cacheResult = (T)func.DynamicInvoke();
                cache.Set<T>(key, cacheGroup, cacheResult);
                return cacheResult;
            }
            else
            {
                return cacheResult;
            }

            #endregion
        }

    }
}
