using log4net;
using Newtonsoft.Json;
using System;

namespace Common.Commons
{
    public class Logger : ILogger
    {
        private readonly ILog _logger = LogManager.GetLogger(typeof(Logger));

        public void LogDebug(string message)
        {
            _logger.Debug(message);
        }

        public void LogError(string message)
        {
            _logger.Error(message);
        }

        public async void LogError(Exception ex)
        {
            _logger.Error(JsonConvert.SerializeObject(ex));
            //await CommonFunc.LogErrorToKafka(ex);
        }

        public void LogInfo(string message)
        {
            _logger.Info(message);
        }

        public void LogWarn(string message)
        {
            _logger.Warn(message);
        }
    }
}
