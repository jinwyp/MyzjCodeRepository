using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Payment
{
    /// <summary>
    /// 支付配置
    /// </summary>
    public class PayConfigs
    {
        /// <summary>
        /// 请求标识
        /// </summary>
        public string RequestIdentity { get; set; }
        /// <summary>
        /// 支付主题
        /// </summary>
        public string Subject { get; set; }
        /// <summary>
        /// 外部订单号
        /// </summary>
        public string OutTradeNo { get; set; }
        /// <summary>
        /// 订单总金额
        /// </summary>
        public string TotalFee { get; set; }
        /// <summary>
        /// 外部买家标识
        /// </summary>
        public string OutUser { get; set; }
    }
}
