using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace ExceptionIntercepts.RealisticSamples
{
    public class ExceptionFinalizer : IExceptionHandler
    {
        public async Task HandleAsync(ExceptionContext context)
        {
            var category = (ExceptionCategory)context.Context.Items["exception.category"];
            dynamic response = context.Context.Items["exception.response"];
            dynamic finalResponse = category.DeveloperMode ? response : response.System;

            context.Context.Response.StatusCode = (int)category.HttpStatus;
            context.Context.Response.ContentType = "application/json";
            await context.Context.Response.WriteAsync((string)JsonConvert.SerializeObject(finalResponse));
        }
    }
}
