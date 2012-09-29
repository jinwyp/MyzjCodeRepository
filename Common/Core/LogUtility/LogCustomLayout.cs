using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Core.LogUtility
{
    /// <summary>
    /// 日志 自定义 布局
    /// </summary>
    public class LogCustomLayout : log4net.Layout.PatternLayout
    {
        /// <summary>
        /// 
        /// </summary>
        public LogCustomLayout()
        {
            AddConverter("system_type", typeof(SystemTypePatternConverter));
            AddConverter("user_id", typeof(UserIdPatternConverter));
            AddConverter("log_code", typeof(LogCodePatternConverter));
            AddConverter("log_desc", typeof(LogDescPatternConverter));
            AddConverter("log_msg", typeof(MessagePatternConverter));
        }



    }
}
