using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Enum
{
    /// <summary>
    /// 支付类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum PaymentNotifyType
    {
        /// <summary>
        /// 支付宝 Wap 回调
        /// </summary>
        Alipay_Wap_Callback=101,
        /// <summary>
        /// 支付宝 Wap 通知
        /// </summary>
        Alipay_Wap_Notify=102
    }
}
