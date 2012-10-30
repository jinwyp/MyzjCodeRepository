using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Caching
{
    /// <summary>
    ///  没有缓存的 虚拟类
    /// </summary>
    public class NotCache : CacheBase, ICache
    {
        private static NotCache _obj;
        private static readonly object LockObj = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static NotCache GetInstance()
        {
            if (_obj == null)
                lock (LockObj)
                    if (_obj == null)
                        _obj = new NotCache();
            return _obj;
        }

        public bool Open()
        {
            return false;
        }

        public bool Close()
        {
            return false;
        }

        public bool Clear()
        {
            return false;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            return false;
        }

        public bool Set<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            return false;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj, DateTime expired)
        {
            return false;
        }

        public bool Add<T>(string key, Enums.MCaching.CacheGroup cacheGroup, T obj)
        {
            return false;
        }

        public T GetValByKey<T>(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            return default(T);
        }

        public Dictionary<string, T> GetValByKeys<T>(List<string> keys, Enums.MCaching.CacheGroup cacheGroup)
        {
            return null;
        }

        public List<string> GetKeys()
        {
            return null;
        }

        public List<string> GetKeys(Enums.MCaching.CacheGroup cacheGroup)
        {
            return null;
        }

        public bool RemoveByKey(string key)
        {
            return false;
        }

        public bool RemoveByKey(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            return false;
        }

        public int RemoveByKeyGroup(Enums.MCaching.CacheGroup cacheGroup)
        {
            return 0;
        }

        public bool Contains(string key, Enums.MCaching.CacheGroup cacheGroup)
        {
            return false;
        }

        public bool Contains(string key)
        {
            return false;
        }
    }
}
