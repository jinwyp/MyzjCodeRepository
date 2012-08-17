using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Product
{
    /// <summary>
    /// 评价列表
    /// </summary>
    [DataContract]
    public class ItemComment
    {
        /// <summary>
        /// 评价id
        /// </summary>
        [DataMember]
        public int? comment_id { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember]
        public int? gid { get; set; }

        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public long? tid { get; set; }

        /// <summary>
        /// 评价人昵称
        /// </summary>
        [DataMember]
        public string nick { get; set; }

        /// <summary>
        /// 评价结果
        /// </summary>
        [DataMember]
        public string result { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [DataMember]
        public DateTime? crated { get; set; }

        /// <summary>
        /// 最后回复时间
        /// </summary>
        [DataMember]
        public DateTime? last_replytime { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [DataMember]
        public string item_title { get; set; }

        /// <summary>
        /// 商品价格
        /// </summary>
        [DataMember]
        public decimal? price { get; set; }

        /// <summary>
        /// 评价内容
        /// </summary>
        [DataMember]
        public string content { get; set; }

        /// <summary>
        /// 回复内容
        /// </summary>
        [DataMember]
        public string reply { get; set; }
    }
}
