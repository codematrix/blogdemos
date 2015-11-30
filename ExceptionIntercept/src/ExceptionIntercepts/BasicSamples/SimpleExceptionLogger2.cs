using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace ExceptionIntercepts.BasicSamples
{
    public class SimpleExceptionLogger2 : IExceptionHandler
    {
        private readonly ILogger _logger;

        public SimpleExceptionLogger2(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<SimpleExceptionLogger2>();
        }

        public Task HandleAsync(ExceptionContext context)
        {
            _logger.LogDebug($"Logging '{context.Exception?.Message}' from '{this.GetType().FullName}'.");
            return Task.FromResult(0);
        }
    }
}
