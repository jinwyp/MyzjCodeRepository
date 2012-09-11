using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EF.Model.Entity
{
    /// <summary>
    /// 订单所有状态
    /// </summary>
    public class OrderAllStatus
    {
        /// <summary>
        /// 订单编码
        /// </summary>
        public string OrderCode { get; set; }
        /// <summary>
        /// 订单号
        /// </summary>
        public int OrderNo { get; set; }
        /// <summary>
        /// 支付状态
        /// </summary>
        public int? PayStatus { get; set; }
        /// <summary>
        /// 订单状态
        /// </summary>
        public int? OrderStatus { get; set; }
    }
}
