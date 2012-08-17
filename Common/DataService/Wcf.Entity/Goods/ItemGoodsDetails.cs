using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Goods
{
    [DataContract]
    public class ItemGoodsDetails
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
        /// 商品属性
        /// </summary>
        [DataMember]
        public List<KeyValuePair<string, object>> attrs { get; set; }

        /// <summary>
        /// 商品描述
        /// </summary>
        [DataMember]
        public string desc { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [DataMember]
        public decimal price { get; set; }

        /// <summary>
        /// 库存
        /// </summary>
        [DataMember]
        public long stock { get; set; }

        /// <summary>
        /// 市场价
        /// </summary>
        [DataMember]
        public decimal marketprice { get; set; }

        /// <summary>
        /// 商品编号
        /// </summary>
        [DataMember]
        public string productcode { get; set; }

        /// <summary>
        /// 积分
        /// </summary>
        [DataMember]
        public int score { get; set; }

    }
}
