using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using System.Threading.Tasks;

namespace ExceptionIntercepts.RealisticSamples
{
    public class ExceptionDbLogger : IExceptionHandler
    {
        public Task HandleAsync(ExceptionContext context)
        {
            var category = (ExceptionCategory)context.Context.Items["exception.category"];
            if (category.Category == ExceptionCategoryType.Unhandled)
            {
                dynamic response = context.Context.Items["exception.response"];

                // log whatever to the Database
                // Note: Application Insights may be a more attractive analytical logger than rolling your own.
            }

            return Task.FromResult(0);
        }
    }
}
