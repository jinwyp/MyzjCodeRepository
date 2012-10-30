using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Caching;

namespace Core.Caching.HttpRuntime
{
    /// <summary>
    /// 
    /// </summary>
    public class MHttpRuntime : CacheBase, ICache
    {
        private static readonly object LockObj = new object();
        private static MHttpRuntime _obj;

        /// <summary>
        /// 获取单例对象
        /// </summary>
        public static MHttpRuntime GetInstance
        {
            get
            {
                if (_obj == null)
                    lock (LockObj)
                        if (_obj == null)
                            _obj = new MHttpRuntime();
                return _obj;
            }
        }

        /// <summary>
        /// 缓存对象
        /// </summary>
        public Cache Cache { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        private MHttpRuntime()
        {
            var httpContext = HttpContext.Current;
            Cache = httpContext.Cache;
        }

        /// <summary>
        /// 打开
        /// </summary>
        /// <returns></returns>
        public bool Open()
        {
            return Cache != null;
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            if (Open())
            {
                Cache = null;
            }
            return Cache == null;
        }

        /// <summary>
        /// 清除所有缓存
        /// </summary>
        /// <returns></returns>
        public bool Clear()
        {
            if (Open())
            {
                try
                {
                    var keys = GetKeys();
                    if (keys != null)
                    {
                        foreach (var key in keys)
                        {
                            Cache.Remove(key);
                        }
                    }
                    return true;
                }
                catch
                {
                    return false;
                }
            }
            return false;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    Cache.Add(cacheKey, obj, null, expired, TimeSpan.Zero, CacheItemPriority.Default, null);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    Cache.Add(cacheKey, obj, null, DateTime.UtcNow.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Default, null);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    Cache.Add(cacheKey, obj, null, expired, TimeSpan.Zero, CacheItemPriority.Default, null);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    Cache.Add(cacheKey, obj, null, DateTime.UtcNow.AddMinutes(30), TimeSpan.Zero, CacheItemPriority.Default, null);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public T GetValByKey<T>(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = default(T);

            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    result = (T)Cache.Get(cacheKey);
                }
                catch
                {
                }
            }
            return result;
        }

        public Dictionary<string, T> GetValByKeys<T>(List<string> keys, Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = new Dictionary<string, T>();

            if (Open())
            {
                try
                {
                    if (keys != null)
                    {
                        foreach (var key in keys)
                        {
                            var cacheKey = FormatKey(key, cacheGroup);
                            result.Add(key, (T)Cache.Get(cacheKey));
                        }
                    }

                }
                catch
                {
                }
            }
            return result;
        }

        public List<string> GetKeys()
        {
            var result = new List<string>();
            if (Open())
            {
                try
                {
                    var cacheEnumerator = Cache.GetEnumerator();
                    while (cacheEnumerator.MoveNext())
                    {
                        result.Add(cacheEnumerator.Key.ToString());
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        public List<string> GetKeys(Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = new List<string>();
            if (Open())
            {
                try
                {
                    var cacheGroupName = cacheGroup.ToString();
                    var cacheEnumerator = Cache.GetEnumerator();
                    while (cacheEnumerator.MoveNext())
                    {
                        var key = cacheEnumerator.Key.ToString();
                        if (key.IndexOf(cacheGroupName, StringComparison.CurrentCultureIgnoreCase) > -1)
                            result.Add(key);
                    }
                }
                catch
                {
                }
            }
            return result;
        }

        public bool RemoveByKey(string key)
        {
            if (Open())
            {
                try
                {
                    Cache.Remove(key);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public bool RemoveByKey(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            if (Open())
            {
                try
                {
                    var cacheKey = FormatKey(key, cacheGroup);
                    Cache.Remove(cacheKey);
                    return true;
                }
                catch
                {

                }
            } return false;
        }

        public int RemoveByKeyGroup(Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = 0;
            if (Open())
            {
                try
                {
                    var keys = GetKeys(cacheGroup);
                    if (keys != null)
                    {
                        foreach (var key in keys)
                        {
                            if (RemoveByKey(key))
                                result++;
                        }
                    }
                }
                catch
                {

                }
            }
            return result;
        }

        public bool Contains(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            var result = false;
            if (Open())
            {
                try
                {
                    var keys = GetKeys(cacheGroup);
                    result = keys.Contains(key);
                }
                catch
                {

                }
            }
            return result;
        }

        public bool Contains(string key)
        {
            var result = false;
            if (Open())
            {
                try
                {
                    var keys = GetKeys();
                    result = keys.Contains(key);
                }
                catch
                {

                }
            }
            return result;
        }

    }
}
