// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Features;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Diagnostics.ExceptionIntercepts
{
    /// <summary>
    /// The Exception Intercept Manager that delegates unhandled exceptions to 
    /// an Exception Handler if defined and zero or more Exception Loggers.
    /// </summary>
    /// <seealso cref="Microsoft.AspNet.Diagnostics.IExceptionInterceptManager" />
    public class ExceptionInterceptManager : IExceptionInterceptManager
    {
        private readonly IList<IExceptionHandler> _exceptionIntercepts;

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionInterceptManager"/> class.
        /// </summary>
        public ExceptionInterceptManager()
        {            
            _exceptionIntercepts = new List<IExceptionHandler>();            
        }

        /// <summary>
        /// Intercepts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.AggregateException"></exception>
        async Task IExceptionInterceptManager.InterceptAsync(HttpContext context)
        {
            var feature = context.Features.Get<IExceptionHandlerFeature>();
            var exceptionContext = new ExceptionContext(context, feature?.Error);
            var handlerExecutionExceptions = new List<Exception>();

            try
            {
                foreach (var handlers in _exceptionIntercepts)
                {
                    try
                    {
                        await handlers.HandleAsync(exceptionContext);
                    }
                    catch (Exception ex)
                    {
                        handlerExecutionExceptions.Add(ex);
                    }
                }
            }
            finally
            {
                if (handlerExecutionExceptions.Any())
                {
                    throw new AggregateException(handlerExecutionExceptions);
                }
            }
        }

        /// <summary>
        /// Adds the exception intercept.
        /// </summary>
        /// <param name="exceptionIntercept">The exception intercept.</param>
        void IExceptionInterceptManager.AddExceptionIntercept(IExceptionHandler exceptionIntercept)
        {
            _exceptionIntercepts.Add(exceptionIntercept);
        }
    }
}
