using System;
using System.Collections.Generic;
using Core.ConfigUtility;
using Core.Enums;
using Core.LogUtility;
using ServiceStack.Redis;

namespace Core.Caching.Redis
{
    /// <summary>
    /// 
    /// </summary>
    public class MRedisCache : CacheBase, ICache
    {
        #region 内部字段
        private static readonly object LockObj = new object();
        private static MRedisCache _obj;
        private static PooledRedisClientManager _cachePool;
        private static IRedisClient Cache { get; set; }

        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        private MRedisCache()
        {
            try
            {
                var onlyReadServers = MConfigManager.GetAppSettingsValue<string>(
                        MConfigManager.FormatKey("RedisServers_readOnly", MConfigs.ConfigsCategory.Cache),
                        "").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                var readWriteServers = MConfigManager.GetAppSettingsValue<string>(
                    MConfigManager.FormatKey("RedisServers_readWrite", MConfigs.ConfigsCategory.Cache),
                    "").Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                _cachePool = new PooledRedisClientManager(readWriteServers, onlyReadServers,
                                                          new RedisClientManagerConfig
                                                          {
                                                              AutoStart = true,
                                                              MaxReadPoolSize = onlyReadServers.Length * 5,
                                                              MaxWritePoolSize = readWriteServers.Length * 5
                                                          });

                //_cacheClient = new RedisClient(host, port);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "初始化 ReidaCache 失败！", ex);
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static MRedisCache GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new MRedisCache();
                return _obj;
            }
        }

        /// <summary>
        /// 打开客户端连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            if (_cachePool != null)
            {
                try
                {
                    if (Cache == null)
                        Cache = _cachePool.GetClient();
                }
                catch (Exception)
                {
                    MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取缓存连接对象失败！");
                    throw new Exception("获取缓存连接对象失败!");
                }
            }
            else
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "连接池初始化失败！");
                throw new Exception("连接池初始化失败!");
            }

            return Cache != null;
        }

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            var result = false;
            try
            {
                if (Open())
                {
                    Cache.Dispose();
                    _cachePool.Dispose();
                    result = true;
                }
            }
            catch
            {
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
                if (Open())
                    Cache.FlushAll();
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public bool Set<T>(string key, MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            var result = false;
            try
            {
                if (Open())
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    result = Cache.Set<T>(cacheKey, obj, expired);
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "设置缓存 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Set<T>(string key, MCaching.CacheGroup cacheGroup, T obj)
        {
            var result = false;
            try
            {
                if (Open())
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    result = Cache.Set<T>(cacheKey, obj);
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "设置缓存 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        public bool Add<T>(string key, MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            var result = false;
            try
            {
                if (Open())
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    result = Cache.Add<T>(cacheKey, obj, expired);
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "添加缓存 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        public bool Add<T>(string key, MCaching.CacheGroup cacheGroup, T obj)
        {
            var result = false;
            try
            {
                if (Open())
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    result = Cache.Add<T>(cacheKey, obj);
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "添加缓存 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取缓存值 来自 缓存Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        public T GetValByKey<T>(string key, MCaching.CacheGroup cacheGroup)
        {
            try
            {
                if (Open())
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    var result = Cache.Get<T>(cacheKey);
                    return result;
                }
            }
            catch (Exception)
            {
            }
            return default(T);
        }

        /// <summary>
        /// 获取缓存值列表 来自 缓存Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        public Dictionary<string, T> GetValByKeys<T>(List<string> keys, MCaching.CacheGroup cacheGroup)
        {
            var result = new Dictionary<string, T>();
            try
            {
                if (Open())
                {

                    var keyList = new List<string>();
                    if (keys != null)
                    {
                        foreach (var key in keys)
                        {
                            var nKey = FormatKey(key, cacheGroup);
                            if (!keyList.Contains(nKey))
                                keyList.Add(nKey);
                        }
                        result = (Dictionary<string, T>)Cache.GetAll<T>(keyList);
                    }
                }
            }
            catch
            {
            }
            return result;
        }

        /// <summary>
        /// 获取所有缓存Key
        /// </summary>
        /// <returns></returns>
        public List<string> GetKeys()
        {
            var result = new List<string>();
            try
            {
                if (Open())
                {
                    result = Cache.GetAllKeys();
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取所有缓存Key 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 获取所有缓存Key 
        /// </summary>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public List<string> GetKeys(MCaching.CacheGroup cacheGroup)
        {
            var result = new List<string>();
            try
            {
                if (Open())
                {
                    var keyList = GetKeys();
                    if (keyList != null && keyList.Count > 0)
                    {
                        foreach (var key in keyList)
                        {
                            if (VerifyKeyInGroup(key, cacheGroup))
                                result.Add(FormatKey(key, cacheGroup));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取缓存key", ex);
            }
            return result;
        }

        /// <summary>
        /// 移除缓存 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool RemoveByKey(string key)
        {
            var result = false;
            try
            {
                if (Open())
                {
                    result = Cache.Remove(key);
                }
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取所有缓存Key 出错！", ex);
            }
            return result;
        }

        /// <summary>
        /// 移除缓存 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        public bool RemoveByKey(string key, MCaching.CacheGroup cacheGroup)
        {
            return RemoveByKey(FormatKey(key, cacheGroup));
        }

        /// <summary>
        /// 移除缓存 来自 缓存分组
        /// </summary>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public int RemoveByKeyGroup(MCaching.CacheGroup cacheGroup)
        {
            var result = 0;
            if (Open())
            {
                var keys = GetKeys();
                foreach (var key in keys)
                {
                    try
                    {
                        if (VerifyKeyInGroup(key, cacheGroup))
                            if (Cache.Remove(key))
                                result++;
                    }
                    catch (Exception ex)
                    {
                        MLogManager.Error(MLogGroup.Other.Redis缓存, null, null, "获取所有缓存Key 出错！", ex);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// 是否存在 该Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        public bool Contains(string key, MCaching.CacheGroup cacheGroup)
        {
            if (Open())
            {
                return GetKeys().Contains(FormatKey(key, cacheGroup));
            }
            return false;
        }

        /// <summary>
        /// 是否存在 该Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(string key)
        {
            if (Open())
                return GetKeys().Contains(key);
            return false;
        }

    }
}
