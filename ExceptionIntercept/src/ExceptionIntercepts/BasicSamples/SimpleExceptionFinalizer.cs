using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using Microsoft.AspNet.Http;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ExceptionIntercepts.BasicSamples
{
    public class SimpleExceptionFinalizer : IExceptionHandler
    {
        public async Task HandleAsync(ExceptionContext context) 
        {
            // sample response
            var response = new
            {
                TicketId = Guid.NewGuid(),
                Timestamp = DateTime.UtcNow.ToString(),
                Message = "You are unauthorized to access this resource."
            };

            context.Context.Response.StatusCode = (int)HttpStatusCode.Forbidden;
            context.Context.Response.ContentType = "application/json";
            await context.Context.Response.WriteAsync(JsonConvert.SerializeObject(response));
        }
    }
}
