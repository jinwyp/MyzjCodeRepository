using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Enyim.Caching;
using Core.LogUtility;
using Core.Enums;
using Enyim.Caching.Memcached;

namespace Core.Caching.Memcached
{
    /// <summary>
    /// 
    /// </summary>
    public class MMemcache : CacheBase, ICache
    {
        private static readonly object LockObj = new object();
        private static MMemcache _obj;
        private static MemcachedClient _cacheClient;

        /// <summary>
        /// 获取单例实例
        /// </summary>
        /// <returns></returns>
        public static MMemcache GetInstance()
        {
            if (_obj == null)
                lock (LockObj)
                    if (_obj == null)
                        _obj = new MMemcache();
            return _obj;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        private MMemcache()
        {
            try
            {
                _cacheClient = new MemcachedClient();
                //_cacheClient.FlushAll();
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "缓存初始化失败！", ex);
            }
        }

        /// <summary>
        /// 打开客户端连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            var result = _cacheClient != null;
            return result;
        }

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            var result = true;
            try
            {
                _cacheClient.Dispose();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            var result = true;
            try
            {
                _cacheClient.FlushAll();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            var result = false;

            try
            {
                var cacheKey = FormatKey(key, cacheGroup);
                result = _cacheClient.Store(StoreMode.Set, cacheKey, obj, expired);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "设置缓存 出错！", ex);
            }

            return result;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            var result = false;

            try
            {
                var cacheKey = FormatKey(key, cacheGroup);
                result = _cacheClient.Store(StoreMode.Set, cacheKey, obj);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "设置缓存 出错！", ex);
            }

            return result;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            var result = false;

            try
            {
                var cacheKey = FormatKey(key, cacheGroup);
                result = _cacheClient.Store(StoreMode.Add, cacheKey, obj, expired);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "添加缓存 出错！", ex);
            }

            return result;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            var result = false;

            try
            {
                var cacheKey = FormatKey(key, cacheGroup);
                result = _cacheClient.Store(StoreMode.Add, cacheKey, obj);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "添加缓存 出错！", ex);
            }

            return result;
        }

        public T GetValByKey<T>(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = default(T);
            try
            {
                var cacheKey = FormatKey(key, cacheGroup);
                result = _cacheClient.Get<T>(cacheKey);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "获取缓存值 出错！", ex);
            }
            return result;
        }

        public Dictionary<string, T> GetValByKeys<T>(List<string> keys, Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = new Dictionary<string, T>();
            if (keys != null && keys.Count > 0)
            {
                foreach (var key in keys)
                {
                    try
                    {
                        result.Add(key, GetValByKey<T>(key, cacheGroup));
                    }
                    catch (Exception ex)
                    {
                        MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取缓存值 出错！", ex);
                    }
                }
            }
            return result;
        }

        public List<string> GetKeys()
        {
            MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "没有方法实现");
            throw new Exception("没有方法实现");
        }

        public List<string> GetKeys(Enums.MCaching.CacheGroup cacheGroup)
        {
            MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "没有方法实现");
            throw new Exception("没有方法实现");
        }

        public bool RemoveByKey(string key)
        {
            var result = false;

            try
            {
                result = _cacheClient.Remove(key);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "移除缓存出错！", ex);
            }

            return result;
        }

        public bool RemoveByKey(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            return RemoveByKey(FormatKey(key, cacheGroup));
        }

        public int RemoveByKeyGroup(Enums.MCaching.CacheGroup cacheGroup)
        {
            MLogManager.Error(MLogGroup.Other.Memcached缓存, null, null, "没有方法实现");
            throw new Exception("没有方法实现");
        }

        public bool Contains(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            var val = GetValByKey<object>(key, cacheGroup);
            return val != null;
        }

    }
}
