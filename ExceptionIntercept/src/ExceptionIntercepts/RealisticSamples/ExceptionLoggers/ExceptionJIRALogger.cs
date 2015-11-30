using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using System.Threading.Tasks;

namespace ExceptionIntercepts.RealisticSamples
{
    public class ExceptionJIRALogger : IExceptionHandler
    {
        public Task HandleAsync(ExceptionContext context)
        {
            var category = (ExceptionCategory)context.Context.Items["exception.category"];
            if (category.Category == ExceptionCategoryType.Unhandled)
            {
                dynamic response = context.Context.Items["exception.response"];

                // log whatever to the JIRA for production issue tracking
            }

            return Task.FromResult(0);
        }
    }
}
