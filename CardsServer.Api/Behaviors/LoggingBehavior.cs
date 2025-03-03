using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace CardsServer.Api.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation($"Before execution for {typeof(TRequest).Name} : {DateTime.Now}");
                return await next();
            }
            finally
            {
                _logger.LogInformation($"After execution for {typeof(TRequest).Name} : {DateTime.Now}");
            }
        }
    }
}
