using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.BaseData
{
    /// <summary>
    /// 支付项目
    /// </summary>
    [Serializable]
    [DataContract]
    public class ItemPay
    {
        /// <summary>
        /// 支付id
        /// </summary>
        [DataMember]
        public int payid { get; set; }

        /// <summary>
        /// 支付名称
        /// </summary>
        [DataMember]
        public string payname { get; set; }

        /// <summary>
        /// 支付类型
        /// </summary>
        [DataMember]
        public int paytype { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        [DataMember]
        public string icon { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        [DataMember]
        public string remark { get; set; }

        /// <summary>
        /// 支付url
        /// </summary>
        [DataMember]
        public string payurl { get; set; }
    }
}
