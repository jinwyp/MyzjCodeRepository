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
            this.AddConverter("system_type", typeof(SystemTypePatternConverter));
            this.AddConverter("user_id", typeof(UserIdPatternConverter));
            this.AddConverter("log_code", typeof(LogCodePatternConverter));
            this.AddConverter("log_desc", typeof(LogDescPatternConverter));
        }



    }
}
