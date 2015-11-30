using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;

namespace ExceptionIntercepts.RealisticSamples
{
    public class ExceptionInspector : IExceptionHandler
    {
        private readonly ExceptionCategorizer _exceptionCategorizer;

        public ExceptionInspector(ExceptionCategorizer exceptionCategorizer)
        {
            _exceptionCategorizer = exceptionCategorizer;
        }

        public Task HandleAsync(ExceptionContext context)
        {
            var category = _exceptionCategorizer.Categorizer(context.Exception);
            dynamic response = new ExpandoObject();

            response.System = new Dictionary<string, string>();
            response.System["Tracking Id"] = Guid.NewGuid().ToString();
            response.System["Timestamp"] = DateTimeOffset.Now.ToString();
            response.System["Message"] = category.ErrorMessage;
            response.System["Execution"] = "Global";

            if(context.Context.Request != null)
            {
                response.System["Execution"] = "Request";

                response.Developer = new Dictionary<string, string>();
                response.Developer["RequestMethod"] = context.Context.Request.Method;
                response.Developer["Uri"] = $"{context.Context.Request.Scheme}:{context.Context.Request.Host}{context.Context.Request.Path}";
                response.Developer["ExceptionType"] = context.Exception.GetType().FullName;
                response.Developer["StackTrace"] = context.Exception.StackTrace.Trim();
            }

            context.Context.Items["exception.category"] = category;
            context.Context.Items["exception.response"] = response;

            return Task.FromResult(0);
        }
    }
}
