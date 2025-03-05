using Microsoft.Extensions.Logging;

namespace CardsServer.Api.FileLogger
{
    public class FileLogger : ILogger, IDisposable
    {
        private readonly string _filePath;
        private static readonly Lock _lock = new();
        public FileLogger(string filePath)
        {
            _filePath = filePath;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return this;
        }
        public void Dispose() { }
        public bool IsEnabled(LogLevel logLevel)
        {
            //return logLevel == LogLevel.Trace;
            return true;
        }
        public void Log<TState>(LogLevel logLevel, EventId eventId,
                    TState state, Exception? exception, Func<TState, Exception?, string> formatter)
        {
            lock (_lock)
            {
                File.AppendAllText(_filePath, formatter(state, exception) + Environment.NewLine);
            }
        }
    }
}
