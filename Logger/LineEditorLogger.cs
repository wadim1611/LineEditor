using Serilog;
using System;

namespace LineEditor.Logger
{
    public class LineEditorLogger : ILogger
    {
        private readonly Serilog.Core.Logger _logger;

        public LineEditorLogger()
        {
            _logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File("logs\\LineEditorLogs.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public void Debug(string message)
        {
            _logger.Debug(message);
        }

        public void Debug(Exception ex, string message)
        {
            _logger.Debug(ex, message);
        }

        public void Error(string message)
        {
            _logger.Error(message);
        }

        public void Error(Exception ex, string message)
        {
            _logger.Error(ex, message);
        }

        public void Fatal(string message)
        {
            _logger.Fatal(message);
        }

        public void Fatal(Exception ex, string message)
        {
            _logger.Fatal(ex, message);
        }

        public void Info(string message)
        {
            _logger.Information(message);
        }

        public void Info(Exception ex, string message)
        {
            _logger.Information(ex, message);
        }


        public void Verbose(string message)
        {
            _logger.Verbose(message);
        }

        public void Verbose(Exception ex, string message)
        {
            _logger.Verbose(ex, message);
        }
    }
}
