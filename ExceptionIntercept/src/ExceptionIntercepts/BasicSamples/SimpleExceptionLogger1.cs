using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExceptionIntercepts.BasicSamples
{
    public class SimpleExceptionLogger1 : IExceptionHandler
    {
        private readonly ILogger _logger;

        public SimpleExceptionLogger1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SimpleExceptionLogger1>();
        }

        public Task HandleAsync(ExceptionContext context)
        {
            _logger.LogDebug($"Logging '{context.Exception?.Message}' from '{this.GetType().FullName}'.");
            return Task.FromResult(0);
        }
    }
}
