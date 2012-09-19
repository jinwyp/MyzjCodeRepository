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
        ExceptionError = -100,
        /// <summary>
        /// 参数错误
        /// </summary>
        ParamsError=-90,
        /// <summary>
        /// 逻辑错误
        /// </summary>
        LogicError=-80,
        /// <summary>
        /// 执行错误
        /// </summary>
        ExecutionError=-70,
        /// <summary>
        /// 需要登录
        /// </summary>
        NeedLogin=-1,
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
