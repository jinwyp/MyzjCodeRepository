using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
        public enum TitleType
        {
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
