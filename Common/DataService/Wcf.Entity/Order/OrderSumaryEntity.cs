using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 订单汇总信息
    /// </summary>
    [DataContract]
    public class OrderSumaryEntity
    {
        /// <summary>
        /// 总积分
        /// </summary>
        public int TotalScore { get; set; }
        /// <summary>
        /// 订单商品总金额
        /// </summary>
        public decimal TotalGoodsFee { get; set; }
        /// <summary>
        /// 总重量
        /// </summary>
        public int TotalWeight { get; set; }
        /// <summary>
        /// 优惠前总金额
        /// </summary>
        public decimal TotalOriginal { get; set; }
        /// <summary>
        /// 总运费
        /// </summary>
        public decimal TotalFreight { get; set; }
        /// <summary>
        /// 总优惠金额
        /// </summary>
        public decimal TotalDiscountFee { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public decimal TotalOrderFee { get; set; }
    }
}
