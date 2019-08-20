using Microsoft.Extensions.Logging;
using System;

namespace DNetBot.Helpers
{
    public static class Formatter
    {
        public static LogMessage GenerateLog(Discord.LogMessage message)
        {
            var logItem = "Time: " + DateTime.Now.ToString();
            logItem += " | " + "Severity: " + message.Severity.ToString();
            logItem += " | " + "Source: " + message.Source;
            logItem += " | " + "Message: " + message.Message;
            if (message.Exception != null)
                logItem += " | " + "Exception: " + message.Exception;

            var logmsg = new LogMessage();
            logmsg.message = logItem;

            if (message.Severity == Discord.LogSeverity.Critical)
                logmsg.level = LogLevel.Critical;
            else if (message.Severity == Discord.LogSeverity.Error)
                logmsg.level = LogLevel.Error;
            else if (message.Severity == Discord.LogSeverity.Warning)
                logmsg.level = LogLevel.Warning;
            else if (message.Severity == Discord.LogSeverity.Info)
                logmsg.level = LogLevel.Information;
            else if (message.Severity == Discord.LogSeverity.Debug)
                logmsg.level = LogLevel.Debug;
            else if (message.Severity == Discord.LogSeverity.Verbose)
                logmsg.level = LogLevel.Trace;

            return logmsg;
        }

        public static void GenerateLog(ILogger logger, Discord.LogSeverity severity, string source, string message, Exception exception = null)
        {
            var logItem = "Time: " + DateTime.Now.ToString();
            logItem += " | " + "Severity: " + severity.ToString();
            logItem += " | " + "Source: " + source;
            logItem += " | " + "Message: " + message;
            if (exception != null)
                logItem += " | " + "Exception: " + exception;

            if (severity == Discord.LogSeverity.Critical)
                logger.Log(LogLevel.Critical, logItem);
            else if (severity == Discord.LogSeverity.Error)
                logger.Log(LogLevel.Error, logItem);
            else if (severity == Discord.LogSeverity.Warning)
                logger.Log(LogLevel.Warning, logItem);
            else if (severity == Discord.LogSeverity.Info)
                logger.Log(LogLevel.Information, logItem);
            else if (severity == Discord.LogSeverity.Debug)
                logger.Log(LogLevel.Debug, logItem);
            else if (severity == Discord.LogSeverity.Verbose)
                logger.Log(LogLevel.Trace, logItem);
        }
    }

    public class LogMessage
    {
        public LogLevel level;
        public string message;
    }
}
