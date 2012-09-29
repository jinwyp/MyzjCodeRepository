using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Enums;
using log4net;
using System.IO;

namespace Core.LogUtility
{
    /// <summary>
    /// 日志类
    /// </summary>
    public static class MLogManager
    {
        /// <summary>
        /// 格式化消息内容
        /// </summary>
        /// <param name="group"></param>
        /// <param name="sid"></param>
        /// <param name="msg"></param>
        private static void FormatMsg(Enum group, string sid, ref string msg)
        {
            sid = string.IsNullOrEmpty(sid) ? "system" : sid;
            msg = string.Format("[系统:{0};描述:{1};代码:{2}]{3}", sid, group, Convert.ToInt64(group), msg);
        }

        /// <summary>
        /// 写调试日志
        /// </summary>
        /// <param name="group">MLogGroup.x</param>
        /// <param name="sid"> </param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Debug(Enum group, string sid, string userId, string msg, params object[] args)
        {
            MLog4Net.GetInstance().Debug(sid, userId, Convert.ToInt64(group), group.ToString(), msg, args);
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="group">MLogGroup.x</param>
        /// <param name="sid"> </param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Warn(Enum group, string sid, string userId, string msg, params Exception[] args)
        {
            MLog4Net.GetInstance().Warn(sid, userId, Convert.ToInt64(group), group.ToString(), msg, args);
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        /// <param name="group">MLogGroup.x</param>
        /// <param name="sid"> </param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public static void Info(Enum group, string sid, string userId, string msg, params object[] args)
        {
            MLog4Net.GetInstance().Info(sid, userId, Convert.ToInt64(group), group.ToString(), msg, args);
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="group">MLogGroup.x</param>
        /// <param name="sid"> </param>
        /// <param name="userId"> </param>
        /// <param name="msg"></param>
        /// <param name="ex"></param>
        public static void Error(Enum group, string sid, string userId, string msg, params Exception[] ex)
        {
            MLog4Net.GetInstance().Error(sid, userId, Convert.ToInt64(group), group.ToString(), msg, ex);
        }
    }
}
