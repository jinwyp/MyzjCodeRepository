using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Goods
{
    /// <summary>
    /// 商品搜索结构
    /// </summary>
    [DataContract]
    public class ItemGoodsSearch
    {
        /// <summary>
        /// 商品Id
        /// </summary>
        [DataMember]
        public int gid { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// 商品图片
        /// </summary>
        [DataMember]
        public string pic_url { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [DataMember]
        public decimal price { get; set; }
    }
}
