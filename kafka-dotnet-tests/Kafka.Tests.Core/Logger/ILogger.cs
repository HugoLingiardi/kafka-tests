namespace Kafka.Tests.Core.Logger
{
    public enum LogType { Information, Error }

    public interface ILogger
    {
        void Log(LogType logType, string text);
        void LogInformation(string text);
        void LogError(string text);
    }
}