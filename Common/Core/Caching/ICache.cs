using System;
using System.Collections.Generic;
using Core.Enums;

namespace Core.Caching
{
    /// <summary>
    /// 
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 打开客户端连接
        /// </summary>
        /// <returns></returns>
        bool Open();

        /// <summary>
        /// 关闭客户端连接
        /// </summary>
        /// <returns></returns>
        bool Close();

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <returns></returns>
        bool Clear();

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool Set(string key, MCaching.CacheGroup cacheGroup, object obj, DateTime expired);

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Set(string key, MCaching.CacheGroup cacheGroup, object obj);

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool Set<T>(string key, MCaching.CacheGroup cacheGroup, T obj, DateTime expired);

        /// <summary>
        /// 设置缓存 如果存在则更新，否则新增
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Set<T>(string key, MCaching.CacheGroup cacheGroup, T obj);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool Add(string key, MCaching.CacheGroup cacheGroup, object obj, DateTime expired);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Add(string key, MCaching.CacheGroup cacheGroup, object obj);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <param name="expired"></param>
        /// <returns></returns>
        bool Add<T>(string key, MCaching.CacheGroup cacheGroup, T obj, DateTime expired);

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Add<T>(string key, MCaching.CacheGroup cacheGroup, T obj);

        /// <summary>
        /// 获取缓存值 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object GetValByKey(string key);

        /// <summary>
        /// 获取缓存值 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        object GetValByKey(string key, MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 获取缓存值 来自 缓存Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T GetValByKey<T>(string key);

        /// <summary>
        /// 获取缓存值 来自 缓存Key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        T GetValByKey<T>(string key, MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 获取缓存值列表 来自 缓存Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <returns></returns>
        Dictionary<string, T> GetValByKeys<T>(List<string> keys);

        /// <summary>
        /// 获取缓存值列表 来自 缓存Keys
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="keys"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        Dictionary<string, T> GetValByKeys<T>(List<string> keys, MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 获取所有缓存Key
        /// </summary>
        /// <returns></returns>
        List<string> GetKeys();

        /// <summary>
        /// 获取所有缓存Key 
        /// </summary>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        List<string> GetKeys(MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 移除缓存 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveByKey(string key);

        /// <summary>
        /// 移除缓存 来自 缓存Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"> </param>
        /// <returns></returns>
        bool RemoveByKey(string key, MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 移除缓存 来自 缓存分组
        /// </summary>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        int RemoveByKeyGroup(MCaching.CacheGroup cacheGroup);

        /// <summary>
        /// 是否存在 该Key
        /// </summary>
        /// <param name="key"></param>
        /// <param name="cacheGroup"></param>
        /// <returns></returns>
        bool Contains(string key, MCaching.CacheGroup cacheGroup);

    }
}
