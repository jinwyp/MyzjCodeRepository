using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 订单明细，商品信息
    /// </summary>
    [DataContract]
    public class ItemOrderDetails
    {

        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public long? oid { get; set; }

        /// <summary>
        /// 商品ID
        /// </summary>
        [DataMember]
        public int? gid { get; set; }

        /// <summary>
        /// 商品标题
        /// </summary>
        [DataMember]
        public string title { get; set; }

        /// <summary>
        /// 成本价
        /// </summary>
        [DataMember]
        public decimal? costprice { get; set; }

        /// <summary>
        /// 销售价
        /// </summary>
        [DataMember]
        public decimal? price { get; set; }

        /// <summary>
        /// 套餐ID
        /// </summary>
        [DataMember]
        public int? meal_id { get; set; }

        /// <summary>
        /// 套餐名称
        /// </summary>
        [DataMember]
        public string meal_name { get; set; }

        /// <summary>
        /// sku id
        /// </summary>
        [DataMember]
        public string sku_id { get; set; }

        /// <summary>
        /// sku 名称
        /// </summary>
        [DataMember]
        public string sku_name { get; set; }

        /// <summary>
        /// 商品数量
        /// </summary>
        [DataMember]
        public int? num { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        [DataMember]
        public decimal? discount_fee { get; set; }

        /// <summary>
        /// 调整金额
        /// </summary>
        [DataMember]
        public decimal? adjust_fee { get; set; }

        /// <summary>
        /// 售后金额
        /// </summary>
        [DataMember]
        public decimal? aftersales_fee { get; set; }

        /// <summary>
        /// 最后修改时间
        /// </summary>
        [DataMember]
        public DateTime? modified { get; set; }

    }
}
