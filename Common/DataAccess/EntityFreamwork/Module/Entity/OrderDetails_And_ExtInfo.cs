using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    /// <summary>
    /// 订单扩展信息
    /// </summary>
    public class OrderDetails_ExtInfo
    {
        /// <summary>
        /// 订单id
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 订单编号
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 收货人
        /// </summary>
        public string Consignee { get; set; }
        /// <summary>
        /// 收货地址信息
        /// </summary>
        public string AddressInfo { get; set; }
        /// <summary>
        /// 邮编
        /// </summary>
        public string Zip { get; set; }
        /// <summary>
        /// 电话
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 配送方式
        /// </summary>
        public string DeliveryType { get; set; }

        /// <summary>
        /// 支付方式
        /// </summary>
        public int PayId { get; set; }
        /// <summary>
        /// 支付状态Id
        /// </summary>
        public int PayStatusId { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public string PayStatus { get; set; }
        /// <summary>
        /// 发票类型
        /// </summary>
        public int InvoiceType { get; set; }
        /// <summary>
        /// 发票分类
        /// </summary>
        public int? InvoiceCategory { get; set; }
        /// <summary>
        /// 发票抬头
        /// </summary>
        public string InvoiceTitle { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public string OrderStatus { get; set; }
        /// <summary>
        /// 订单状态Id
        /// </summary>
        public int OrderStatusId { get; set; }
        /// <summary>
        /// 支付方式
        /// </summary>
        public string PayType { get; set; }
        /// <summary>
        /// 省份
        /// </summary>
        public string Provinces { get; set; }
        /// <summary>
        /// 城市
        /// </summary>
        public string City { get; set; }
        /// <summary>
        /// 区县
        /// </summary>
        public string County { get; set; }
        /// <summary>
        /// 手机
        /// </summary>
        public string Mobile { get; set; }
    }
}
