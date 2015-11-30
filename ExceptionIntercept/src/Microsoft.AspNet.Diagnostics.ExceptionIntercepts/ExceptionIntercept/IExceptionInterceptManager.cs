// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNet.Http;
using System.Threading.Tasks;

namespace Microsoft.AspNet.Diagnostics.ExceptionIntercepts
{
    /// <summary>
    /// IExceptionInterceptManager.
    /// </summary>
    public interface IExceptionInterceptManager
    {
        /// <summary>
        /// Intercepts the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        Task InterceptAsync(HttpContext context);

        /// <summary>
        /// Adds the exception intercept.
        /// </summary>
        /// <param name="exceptionIntercept">The exception intercept.</param>
        void AddExceptionIntercept(IExceptionHandler exceptionIntercept);
    }
}
