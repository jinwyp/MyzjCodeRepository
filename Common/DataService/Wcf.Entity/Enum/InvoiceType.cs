using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Wcf.Entity.Enum
{
    /// <summary>
    /// 发票
    /// </summary>
    public class Invoice
    {
        /// <summary>
        /// 发票title 类型
        /// </summary>
        [Serializable]
        [DataContract]
        public enum TitleType
        {
            /// <summary>
            /// 不需要
            /// </summary>
            NoNeed = 0,
            /// <summary>
            /// 个人
            /// </summary>
            Personal = 1,
            /// <summary>
            /// 公司
            /// </summary>
            Company = 2
        }
        /// <summary>
        /// 发票分类
        /// </summary>
        [Serializable]
        [DataContract]
        public enum InvoiceCategory
        {
            /// <summary>
            /// 用品
            /// </summary>
            Food = 1,
            /// <summary>
            /// 食品
            /// </summary>
            Articles = 2
        }
    }
}
