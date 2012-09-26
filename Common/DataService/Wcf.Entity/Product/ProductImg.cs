using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Goods
{
    /// <summary>
    /// 产品属性图片
    /// </summary>
    [Serializable]
    [DataContract]
    public class ProductImg
    {
        /// <summary>
        /// 图片ID
        /// </summary>
        [DataMember]
        public int? id { get; set; }

        /// <summary>
        /// 产品ID
        /// </summary>
        [DataMember]
        public int? product_id { get; set; }

        /// <summary>
        /// 图片连接地址
        /// </summary>
        [DataMember]
        public string url { get; set; }

        /// <summary>
        /// 图片位置
        /// </summary>
        [DataMember]
        public int? position { get; set; }

        /// <summary>
        /// 图片创建时间
        /// </summary>
        [DataMember]
        public DateTime? created { get; set; }
    }
}
