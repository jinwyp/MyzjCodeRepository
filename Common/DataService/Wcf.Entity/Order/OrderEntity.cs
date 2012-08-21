using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wcf.Entity.Enum;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 订单数据结构
    /// </summary>
    [DataContract]
    public class OrderEntity
    {
        /// <summary>
        /// 订单ID
        /// </summary>
        [DataMember]
        public long? oid { get; set; }

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
        /// 收货地址ID
        /// </summary>
        [DataMember]
        public int addressid { get; set; }

        /// <summary>
        /// 支付方式id
        /// </summary>
        [DataMember]
        public int? payid { get; set; }

        /// <summary>
        /// 配送方式id
        /// </summary>
        [DataMember]
        public int logisticsid { get; set; }

        /// <summary>
        /// 配送时间类型
        /// </summary>
        [DataMember]
        public int posttimetype { get; set; }

        /// <summary>
        /// 发票抬头类型
        /// </summary>
        [DataMember]
        public Invoice.TitleType? titletype { get; set; }

        /// <summary>
        /// 发票分类
        /// </summary>
        [DataMember]
        public Invoice.InvoiceCategory? invoicecategory { get; set; }

        /// <summary>
        /// 发票抬头
        /// </summary>
        [DataMember]
        public string invoicetitle { get; set; }

        /// <summary>
        /// 订单备注
        /// </summary>
        [DataMember]
        public string remark { get; set; }

    }
}
