namespace Common.Commons
{
    public interface ILogger
    {
        void LogDebug(string message);
        void LogError(Exception ex);
        void LogError(string message);
        void LogInfo(string message);
        void LogWarn(string message);
    }
}