using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Wcf.Entity.Enum;

namespace Wcf.Entity.Order
{
    /// <summary>
    /// 支付通知
    /// </summary>
    [DataContract]
    public class PaymentNotify
    {
        /// <summary>
        /// 支付通知方式
        /// </summary>
        [DataMember]
        public PaymentNotifyType paymentnotifytype { get; set; }
        /// <summary>
        /// get 数据
        /// </summary>
        [DataMember]
        public string getdata { get; set; }
        /// <summary>
        /// post 数据
        /// </summary>
        [DataMember]
        public string postdata { get; set; }
    }
}
