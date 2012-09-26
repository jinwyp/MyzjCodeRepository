using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Order
{
    [Serializable]
    [DataContract]
    public class ItemOrder
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public long? oid { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        [DataMember]
        public string ocode { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int? statusid { get; set; }

        /// <summary>
        /// 订单总额 不包括邮费
        /// </summary>
        [DataMember]
        public decimal? total_fee { get; set; }

        /// <summary>
        /// 运费总额
        /// </summary>
        [DataMember]
        public decimal? total_freight { get; set; }

        /// <summary>
        /// 支付金额
        /// </summary>
        [DataMember]
        public decimal? payment { get; set; }

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

        /// <summary>
        /// 买家昵称
        /// </summary>
        [DataMember]
        public string buyer_nick { get; set; }

        /// <summary>
        /// 买家字符串ID
        /// </summary>
        [DataMember]
        public string buyer_uid { get; set; }

        /// <summary>
        /// 订单创建时间
        /// </summary>
        [DataMember]
        public DateTime? created { get; set; }

        /// <summary>
        /// 订单超时日期
        /// </summary>
        [DataMember]
        public DateTime? timeout_action { get; set; }

        /// <summary>
        /// 买家是否已评价 true 已评价 false 未评价
        /// </summary>
        [DataMember]
        public bool buyer_rate { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [DataMember]
        public string paytype { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        [DataMember]
        public string logisticstype { get; set; }

        /// <summary>
        /// 送货时间
        /// </summary>
        [DataMember]
        public string posttimetype { get; set; }

        /// <summary>
        /// 订单总金额
        /// </summary>
        [DataMember]
        public decimal? total_order { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public string status { get; set; }
    }
}
