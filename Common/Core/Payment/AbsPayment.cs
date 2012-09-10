using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Payment
{
    /// <summary>
    /// 支付 接口定义
    /// </summary>
    public interface IPayment
    {
        /// <summary>
        /// 初始化
        /// </summary>
        /// <returns></returns>
        IPayment Init();

        /// <summary>
        /// 创建请求url
        /// </summary>
        /// <returns></returns>
        string CreateRequestUrl();
    }
}
