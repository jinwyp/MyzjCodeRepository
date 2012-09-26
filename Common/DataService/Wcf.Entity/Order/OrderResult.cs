using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Order
{
    [Serializable]
    [DataContract]
    public class OrderResult
    {
        /// <summary>
        /// 总积分
        /// </summary>
        [DataMember]
        public int total_score { get; set; }


        /// <summary>
        /// 商品总数
        /// </summary>
        [DataMember]
        public int total_goods { get; set; }

        /// <summary>
        /// 总重量
        /// </summary>
        [DataMember]
        public long total_weight { get; set; }

        /// <summary>
        /// 总运费
        /// </summary>
        [DataMember]
        public decimal total_freight { get; set; }

        /// <summary>
        /// 商品总额
        /// </summary>
        [DataMember]
        public decimal total_goods_fee { get; set; }

        /// <summary>
        /// 订单总额
        /// </summary>
        [DataMember]
        public decimal total_order_fee { get; set; }

        /// <summary>
        /// 优惠前总额
        /// </summary>
        [DataMember]
        public decimal total_original { get; set; }

        /// <summary>
        /// 优惠总额
        /// </summary>
        [DataMember]
        public decimal total_discount_fee { get; set; }
    }
}
