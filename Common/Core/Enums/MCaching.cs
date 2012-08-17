using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    public class MCaching
    {
        /// <summary>
        /// 缓存提供器
        /// </summary>
        public enum Provider
        {
            /// <summary>
            /// 默认Web 缓存
            /// </summary>
            HttpRuntime,
            /// <summary>
            /// Key Value 缓存服务器
            /// </summary>
            Redis,
            /// <summary>
            /// 分布式缓存
            /// </summary>
            Memcached
        }

        /// <summary>
        /// 缓存 Key 分组
        /// </summary>
        public enum CacheGroup
        {
            Debug,
            System,
            Member,
            Goods,
            Order
        }

    }
}
