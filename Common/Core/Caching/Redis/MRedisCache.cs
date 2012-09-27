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
        private static IRedisClient _cacheClient;
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
                        "").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                var readWriteServers = MConfigManager.GetAppSettingsValue<string>(
                    MConfigManager.FormatKey("RedisServers_readWrite", MConfigs.ConfigsCategory.Cache),
                    "").Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                _cachePool = new PooledRedisClientManager(readWriteServers, onlyReadServers,
                                                          new RedisClientManagerConfig
                                                          {
                                                              AutoStart = true,
                                                              MaxReadPoolSize = onlyReadServers.Length * 5,
                                                              MaxWritePoolSize = readWriteServers.Length * 5
                                                          });

                //GetClient() = new RedisClient(host, port);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "初始化 ReidaCache 失败！", ex);
            }
        }

        /// <summary>
        /// 从连接池取连接对象
        /// </summary>
        /// <returns></returns>
        public static IRedisClient GetClient()
        {
            //if (_cacheClient != null)
            //{
            //    return _cacheClient;
            //}
            if (_cachePool != null)
            {
                try
                {
                    if (_cacheClient == null)
                        _cacheClient = _cachePool.GetClient();
                    return _cacheClient;
                }
                catch (Exception)
                {
                    MLogManager.Error(MLogGroup.Other.Redis缓存, null, "获取缓存连接对象失败！");
                    throw new Exception("获取缓存连接对象失败!");
                }
            }
            else
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "连接池初始化失败！");
                throw new Exception("连接池初始化失败!");
            }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static MRedisCache GetInstance()
        {
            if (_obj == null)
                lock (LockObj)
                    if (_obj == null)
                        _obj = new MRedisCache();
            return _obj;
        }

        /// <summary>
        /// 打开客户端连接
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            var result = GetClient() != null;
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
                _cachePool.Dispose();
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
                GetClient().FlushDb();
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
                var cacheKey = FormatKey(key, cacheGroup);
                result = GetClient().Set<T>(cacheKey, obj, expired);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "设置缓存 出错！", ex);
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
                var cacheKey = FormatKey(key, cacheGroup);
                result = GetClient().Set<T>(cacheKey, obj);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "设置缓存 出错！", ex);
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
                var cacheKey = FormatKey(key, cacheGroup);
                result = GetClient().Add<T>(cacheKey, obj, expired);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "添加缓存 出错！", ex);
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
                var cacheKey = FormatKey(key, cacheGroup);
                result = GetClient().Add<T>(cacheKey, obj);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "添加缓存 出错！", ex);
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
            var cacheKey = FormatKey(key, cacheGroup);
            var result = GetClient().Get<T>(cacheKey);
            return result;
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
            var keyList = new List<string>();
            if (keys != null)
            {

                foreach (var key in keys)
                {
                    var nKey = FormatKey(key, cacheGroup);
                    if (!keyList.Contains(nKey))
                        keyList.Add(nKey);
                }
                result = (Dictionary<string, T>)GetClient().GetAll<T>(keyList);
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
                result = GetClient().GetAllKeys();
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "获取所有缓存Key 出错！", ex);
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
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "获取缓存key", ex);
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
                result = GetClient().Remove(key);
            }
            catch (Exception ex)
            {
                MLogManager.Error(MLogGroup.Other.Redis缓存, null, "获取所有缓存Key 出错！", ex);
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
            var keys = GetKeys();
            foreach (var key in keys)
            {
                try
                {
                    if (VerifyKeyInGroup(key, cacheGroup))
                        if (GetClient().Remove(key))
                            result++;
                }
                catch (Exception ex)
                {
                    MLogManager.Error(MLogGroup.Other.Redis缓存, null, "获取所有缓存Key 出错！", ex);
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
            return GetKeys().Contains(FormatKey(key, cacheGroup));
        }
    }
}
