using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using log4net.Core;
using log4net.Layout.Pattern;

namespace Core.LogUtility
{
    internal sealed class SystemTypePatternConverter: PatternLayoutConverter  
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logMessage = loggingEvent.MessageObject as LogCustomEntity;
            if (logMessage != null)
                writer.Write(logMessage.SystemType);  
        }
    }
    internal sealed class UserIdPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logMessage = loggingEvent.MessageObject as LogCustomEntity;
            if (logMessage != null)
                writer.Write(logMessage.UserId);
        }
    }
    internal sealed class LogCodePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logMessage = loggingEvent.MessageObject as LogCustomEntity;
            if (logMessage != null)
                writer.Write(logMessage.LogCode);
        }
    }
    internal sealed class LogDescPatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logMessage = loggingEvent.MessageObject as LogCustomEntity;
            if (logMessage != null)
                writer.Write(logMessage.LogDesc);
        }
    }
    internal sealed class MessagePatternConverter : PatternLayoutConverter
    {
        protected override void Convert(TextWriter writer, LoggingEvent loggingEvent)
        {
            var logMessage = loggingEvent.MessageObject as LogCustomEntity;
            if (logMessage != null)
                writer.Write(logMessage.Msg);
        }
    }
}
