using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Wcf.Entity.Enum
{
    /// <summary>
    /// 系统类型
    /// </summary>
    [Serializable]
    [DataContract]
    public enum SystemType
    {
        /// <summary>
        /// 电销
        /// </summary>
        PhoneSale = 100,
        /// <summary>
        /// 网站
        /// </summary>
        WebSite = 102,
        /// <summary>
        /// 手机网站
        /// </summary>
        MobileWebSite = 103
    }
}
