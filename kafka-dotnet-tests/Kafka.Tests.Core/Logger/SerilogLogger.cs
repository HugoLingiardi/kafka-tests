using System;
using Serilog;

namespace Kafka.Tests.Core.Logger
{
    public class SerilogLogger : ILogger
    {
        private readonly Serilog.ILogger serilogLogger;
        public SerilogLogger()
        {
            serilogLogger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
        }

        public void Log(LogType logType, string text)
        {
            switch (logType)
            {
                case LogType.Error:
                    serilogLogger.Error(text);
                    break;
                case LogType.Information:
                    serilogLogger.Information(text);
                    break;
                default:
                    throw new InvalidOperationException("Not such log type.");
            }
        }

        public void LogError(string text)
        {
            Log(LogType.Error, text);
        }

        public void LogInformation(string text)
        {
            Log(LogType.Information, text);
        }
    }
}