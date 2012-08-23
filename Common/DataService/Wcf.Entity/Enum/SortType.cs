using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Enum
{
    /// <summary>
    /// 排序类型
    /// </summary>
    [DataContract]
    public enum SortType
    {
        /// <summary>
        /// 销量 升序
        /// </summary>
        SalesAsc = 100,
        /// <summary>
        /// 销量 降序
        /// </summary>
        SalesDesc = 101,
        /// <summary>
        /// 价格 升序
        /// </summary>
        PriceAsc = 200,
        /// <summary>
        /// 价格 降序
        /// </summary>
        PriceDesc = 201,
        /// <summary>
        /// 上架时间 升序
        /// </summary>
        EnableTimeAsc = 300,
        /// <summary>
        /// 上架时间 降序
        /// </summary>
        EnableTimeDesc = 301
    }
}
