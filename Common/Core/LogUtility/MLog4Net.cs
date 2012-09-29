using System;
using System.Globalization;
using Core.Enums;
using log4net;
using log4net.Appender;
using log4net.Config;
using System.Collections.Generic;
using System.Threading;

namespace Core.LogUtility
{
    /// <summary>
    /// 消息队列实体
    /// </summary>
    public class LogQueueItem
    {
        /// <summary>
        /// 消息类型
        /// </summary>
        public MLoggerType LogType { get; set; }
        /// <summary>
        /// 消息主体
        /// </summary>
        public LogCustomEntity Message { get; set; }
        /// <summary>
        /// 异常信息
        /// </summary>
        public Exception ex { get; set; }
    }

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
        private static MemoryAppender _memoryLogger;
        private static Queue<LogQueueItem> _queue = new Queue<LogQueueItem>();
        private static Thread _queueThread;
        #endregion

        /// <summary>
        /// 构造函数
        /// </summary>
        private MLog4Net()
        {
            try
            {
                log4net.Config.XmlConfigurator.Configure();
                _debug = LogManager.GetLogger("DEBUG");
                _info = LogManager.GetLogger("INFO");
                _error = LogManager.GetLogger("ERROR");
                _warn = LogManager.GetLogger("WARN");

                _memoryLogger = new MemoryAppender();
                BasicConfigurator.Configure(_memoryLogger);

                #region 写入 日志任务

                _queueThread = new Thread(() =>
                                              {
                                                  while (true)
                                                  {
                                                      var size = 20;
                                                      if (_queue.Count > size)
                                                      {
                                                          CommitLog(size);
                                                      }
                                                      Thread.Sleep(60 * 1000 * 5);
                                                  }
                                              });
                _queueThread.IsBackground = true;
                _queueThread.Start();

                #endregion

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
        /// 插入日志
        /// </summary>
        /// <param name="logQueueItem"></param>
        private void WriteLog(LogQueueItem logQueueItem)
        {
            if (logQueueItem != null && logQueueItem.Message != null)
            {
                _queue.Enqueue(logQueueItem);
            }
        }

        /// <summary>
        /// 提交日志
        /// </summary>
        private static void CommitLog(int size)
        {
            for (var i = 0; i < size; i++)
            {
                var queueItem = _queue.Dequeue();
                switch (queueItem.LogType)
                {
                    case MLoggerType.Debug:
                        if (queueItem.ex != null)
                            _debug.Debug(queueItem.Message, queueItem.ex);
                        else
                            _debug.Debug(queueItem.Message);
                        break;
                    case MLoggerType.Error:
                        if (queueItem.ex != null)
                            _debug.Error(queueItem.Message, queueItem.ex);
                        else
                            _debug.Error(queueItem.Message);
                        break;
                    case MLoggerType.Info:
                        _debug.Info(queueItem.Message);
                        break;
                    case MLoggerType.Warn:
                        _debug.Warn(queueItem.Message);
                        break;
                }
            }
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
                    WriteLog(new LogQueueItem
                                       {
                                           Message = message,
                                           LogType = MLoggerType.Debug,
                                           ex = null
                                       });
                    //_debug.Debug(message);
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
                    WriteLog(new LogQueueItem
                    {
                        Message = message,
                        LogType = MLoggerType.Info,
                        ex = null
                    });
                    //_info.Info(message);
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
        /// <param name="ex"> </param>
        public void Warn(string systemType, string userId, Int64 logCode, string logDesc, string msg, Exception[] ex)
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
                    WriteLog(new LogQueueItem
                    {
                        Message = message,
                        LogType = MLoggerType.Warn,
                        ex = ex.Length > 0 ? ex[0] : null
                    });
                    /*
                    if (args.Length > 0)
                        _warn.Warn(message, args[0]);
                    else
                        _warn.Warn(message);
                    */
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
                    WriteLog(new LogQueueItem
                    {
                        Message = message,
                        LogType = MLoggerType.Error,
                        ex = ex.Length > 0 ? ex[0] : null
                    });
                    /*
                    if (ex.Length > 0)
                        _error.Error(message, ex[0]);
                    else
                        _error.Error(message);
                    */
                }
                catch
                { }
            }
        }
    }
}
