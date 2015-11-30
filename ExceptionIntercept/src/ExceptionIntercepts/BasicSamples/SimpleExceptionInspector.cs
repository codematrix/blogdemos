using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using System.Threading.Tasks;

namespace ExceptionIntercepts.BasicSamples
{
    public class SimpleExceptionInspector : IExceptionHandler
    {
        public Task HandleAsync(ExceptionContext context)
        {
            return Task.FromResult(0);
        }
    }
}
