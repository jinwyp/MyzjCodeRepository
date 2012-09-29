﻿using System;
using System.Globalization;
using Core.Enums;
using log4net;

namespace Core.LogUtility
{
    /// <summary>
    /// 
    /// </summary>
    public class MLog4Net
    {
        #region 内部字段
        private static readonly object Lock = new object();
        private static MLog4Net _obj;
        private static ILog _debug, _info, _error, _warn;
        private static bool _isInit;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        private MLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                _debug = log4net.LogManager.GetLogger("DEBUG");
                _info = log4net.LogManager.GetLogger("INFO");
                _error = log4net.LogManager.GetLogger("ERROR");
                _warn = log4net.LogManager.GetLogger("WARN");
                _isInit = true;
            }
            catch
            { }
        }

        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static MLog4Net GetInstance()
        {
            if (_obj == null)
            {
                lock (Lock)
                {
                    if (_obj == null)
                    {
                        _obj = new MLog4Net();
                    }
                }
            }
            return _obj;
        }

        /// <summary>
        /// 写调试日志
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="userId"></param>
        /// <param name="logCode"></param>
        /// <param name="logDesc"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Debug(string systemType, string userId, Int64 logCode, string logDesc, string msg, params object[] args)
        {
            if (_isInit && _warn.IsDebugEnabled)
            {
                try
                {
                    var message = new LogCustomEntity
                    {
                        SystemType = systemType,
                        UserId = userId,
                        LogCode = logCode.ToString(),
                        LogDesc = logDesc,
                        Msg = string.Format(msg, args)
                    };

                    _debug.Debug(message);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 写信息日志
        /// </summary>
        /// <param name="systemType"></param>
        /// <param name="userId"></param>
        /// <param name="logCode"></param>
        /// <param name="logDesc"></param>
        /// <param name="msg"></param>
        /// <param name="args"></param>
        public void Info(string systemType, string userId, Int64 logCode, string logDesc, string msg, params object[] args)
        {
            if (_isInit && _warn.IsInfoEnabled)
            {
                try
                {
                    var message = new LogCustomEntity
                    {
                        SystemType = systemType,
                        UserId = userId,
                        LogCode = logCode.ToString(),
                        LogDesc = logDesc,
                        Msg = string.Format(msg, args)
                    };

                    _info.Info(message);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 写警告日志
        /// </summary>
        /// <param name="logDesc"> </param>
        /// <param name="msg"></param>
        /// <param name="systemType"> </param>
        /// <param name="userId"> </param>
        /// <param name="logCode"> </param>
        /// <param name="args"> </param>
        public void Warn(string systemType, string userId, Int64 logCode, string logDesc, string msg, Exception[] args)
        {
            if (_isInit && _warn.IsWarnEnabled)
            {
                try
                {
                    var message = new LogCustomEntity
                                      {
                                          SystemType = systemType,
                                          UserId = userId,
                                          LogCode = logCode.ToString(),
                                          LogDesc = logDesc,
                                          Msg = msg
                                      };

                    if (args.Length > 0)
                        _warn.Warn(message, args[0]);
                    else
                        _warn.Warn(message);
                }
                catch
                { }
            }
        }

        /// <summary>
        /// 写错误日志
        /// </summary>
        /// <param name="logDesc"> </param>
        /// <param name="msg"></param>
        /// <param name="systemType"> </param>
        /// <param name="userId"> </param>
        /// <param name="logCode"> </param>
        /// <param name="ex"></param>
        public void Error(string systemType, string userId, Int64 logCode, string logDesc, string msg, params Exception[] ex)
        {
            if (_isInit && _warn.IsErrorEnabled)
            {
                try
                {
                    var message = new LogCustomEntity
                                      {
                                          SystemType = systemType,
                                          UserId = userId,
                                          LogCode = logCode.ToString(CultureInfo.InvariantCulture),
                                          LogDesc = logDesc,
                                          Msg = msg
                                      };

                    if (ex.Length > 0)
                        _error.Error(message, ex[0]);
                    else
                        _error.Error(message);
                }
                catch
                { }
            }
        }
    }
}
