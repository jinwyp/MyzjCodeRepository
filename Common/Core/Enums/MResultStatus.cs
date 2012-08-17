using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.Enums
{
    /// <summary>
    /// 执行状态
    /// </summary>
    public enum MResultStatus
    {
        /// <summary>
        /// 异常错误
        /// </summary>
        ExceptionError = -4,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParamsError = -3,
        /// <summary>
        /// 逻辑错误
        /// </summary>
        LogicError = -2,
        /// <summary>
        /// 执行错误
        /// </summary>
        ExecutionError = -1,
        /// <summary>
        /// 未定义
        /// </summary>
        Undefined = 0,
        /// <summary>
        /// 成功
        /// </summary>
        Success = 1
    }
}
