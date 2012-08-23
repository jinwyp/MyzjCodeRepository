using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using Wcf.Entity.Enum;

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
        public long oid { get; set; }

        /// <summary>
        /// 订单编码
        /// </summary>
        [DataMember]
        public string ocode { get; set; }

        /// <summary>
        /// 优惠金额
        /// </summary>
        [DataMember]
        public decimal discount_fee { get; set; }

        /// <summary>
        /// 调整金额
        /// </summary>
        [DataMember]
        public decimal adjust_fee { get; set; }

        /// <summary>
        /// 售后金额
        /// </summary>
        [DataMember]
        public decimal aftersales_fee { get; set; }

        #region 收货信息
        /// <summary>
        /// 联系人姓名
        /// </summary>
        [DataMember]
        public string contact_name { get; set; }

        /// <summary>
        /// 省份
        /// </summary>
        [DataMember]
        public string province { get; set; }

        /// <summary>
        /// 城市
        /// </summary>
        [DataMember]
        public string city { get; set; }

        /// <summary>
        /// 区县
        /// </summary>
        [DataMember]
        public string county { get; set; }

        /// <summary>
        /// 详细街道地址
        /// </summary>
        [DataMember]
        public string addr { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [DataMember]
        public string zip { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [DataMember]
        public string phone { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string mobile { get; set; }
        #endregion

        /// <summary>
        /// 配送方式
        /// </summary>
        [DataMember]
        public string deliverytype { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        [DataMember]
        public string paytype { get; set; }
        /// <summary>
        /// 支付状态Id
        /// </summary>
        [DataMember]
        public int paystatusid { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        [DataMember]
        public string paystatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public string status { get; set; }

        /// <summary>
        /// 订单状态
        /// </summary>
        [DataMember]
        public int statusid { get; set; }

        #region 发票信息

        /// <summary>
        /// 发票抬头类型
        /// </summary>
        [DataMember]
        public int titletype { get; set; }

        /// <summary>
        /// 发票分类
        /// </summary>
        [DataMember]
        public string invoicecategory { get; set; }

        /// <summary>
        /// 发票抬头
        /// </summary>
        [DataMember]
        public string invoicetitle { get; set; }

        #endregion

    }
}
