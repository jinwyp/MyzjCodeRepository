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
        /// 是否启用缓存
        /// </summary>
        private static readonly bool IsEnable = false;

        static MCacheManager()
        {
            IsEnable = MConfigManager.GetAppSettingsValue<bool>(MConfigManager.FormatKey("IsEnable", MConfigs.ConfigsCategory.Cache), false);
        }

        /// <summary>
        /// 获取缓存对象 自定义缓存类型
        /// </summary>
        /// <returns></returns>
        public static ICache GetCacheObj(MCaching.Provider provider)
        {
            if (_cacheObj == null)
            {
                if (IsEnable)
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
                                MLogManager.Error(MLogGroup.Other.获取缓存对象, "", "",
                                                  "缓存方式错误 没有指定 " + MCaching.Provider.Memcached + " 缓存的实现方法！");
                            }
                            break;
                    }
                }
                else
                {
                    _cacheObj = NotCache.GetInstance();
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
                if (IsEnable)
                {
                    var key = MConfigManager.FormatKey("Default", MConfigs.ConfigsCategory.Cache);
                    var cacheType = MConfigManager.GetAppSettingsValue(key, "");
                    if (!string.IsNullOrEmpty(cacheType))
                    {
                        if (cacheType.Equals(MCaching.Provider.Redis.ToString(), StringComparison.OrdinalIgnoreCase))
                            _cacheObj = MRedisCache.GetInstance();
                        else if (cacheType.Equals(MCaching.Provider.Memcached.ToString(),
                                                  StringComparison.OrdinalIgnoreCase))
                            _cacheObj = MMemcache.GetInstance();
                        else
                            MLogManager.Error(MLogGroup.Other.获取缓存对象, null, "", "缓存方式配置无法识别，节点" + key);
                    }
                    else
                    {
                        MLogManager.Error(MLogGroup.Other.获取缓存对象, null, "", "请配置缓存方式，节点" + key);
                    }
                }
                else
                {
                    _cacheObj = NotCache.GetInstance();
                }
            }
            return _cacheObj;
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T UseCached<T>(string key, MCaching.CacheGroup cacheGroup, Func<object> func) where T : class
        {
            return UseCached<T>(key, cacheGroup, DateTime.Now.AddMinutes(30), GetCacheObj(), func);
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <param name="expired"> </param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static T UseCached<T>(string key, MCaching.CacheGroup cacheGroup, DateTime expired, Func<object> func) where T : class
        {
            return UseCached<T>(key, cacheGroup, expired, GetCacheObj(), func);
        }

        /// <summary>
        /// 使用缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <param name="expired"> </param>
        /// <param name="cache"></param>
        /// <param name="func"></param>
        public static T UseCached<T>(string key, MCaching.CacheGroup cacheGroup, DateTime expired, ICache cache, Func<object> func) where T : class
        {
            try
            {
                T cacheResult;
                if (cache.Contains(key, cacheGroup))
                {
                    cacheResult = (T)cache.GetValByKey<T>(key, cacheGroup);
                    return cacheResult;
                }
                else
                {
                    cacheResult = func.Invoke() as T;
                    cache.Set<T>(key, cacheGroup, cacheResult, expired);
                    return cacheResult;
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, "", null, "使用缓存数据出错！", ex);
            }
            return null;
        }

        /// <summary>
        /// 刷新缓存分组版本
        /// </summary>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public static string RefreshCacheGroupVersion(MCaching.CacheGroup cacheGroup)
        {
            var cacheKey = cacheGroup + "_Version";
            var cache = GetCacheObj();
            var versionId = DateTime.Now.ToString("yyyyMMddHHmmssffff");
            if (cache.Set(cacheKey, MCaching.CacheGroup.Cache, versionId))
                return versionId;
            else
                return null;
        }

    }
}
