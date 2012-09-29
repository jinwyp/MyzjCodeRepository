using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Core.LogUtility
{
    /// <summary>
    /// 日志 自定义实体
    /// </summary>
    public class LogCustomEntity
    {
        /// <summary>
        /// 消息
        /// </summary>
        public string Msg { get; set; }
        /// <summary>
        /// 系统Id
        /// </summary>
        public string SystemType { get; set; }
        /// <summary>
        /// 用户id
        /// </summary>
        public string UserId { get; set; }
        /// <summary>
        /// 日志 编码
        /// </summary>
        public string LogCode { get; set; }
        /// <summary>
        /// 日志 描述
        /// </summary>
        public string LogDesc { get; set; }
    }
}
