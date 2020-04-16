using System;

namespace LineEditor.Logger
{
    public interface ILogger
    {
        void Debug(string message);
        void Debug(Exception ex, string message);

        void Info(string message);
        void Info(Exception ex, string message);

        void Error(string message);
        void Error(Exception ex, string message);

        void Fatal(string message);
        void Fatal(Exception ex, string message);

        void Verbose(string message);
        void Verbose(Exception ex, string message);
    }
}
