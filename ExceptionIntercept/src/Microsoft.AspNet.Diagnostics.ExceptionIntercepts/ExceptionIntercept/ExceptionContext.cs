// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNet.Http;
using System;

namespace Microsoft.AspNet.Diagnostics.ExceptionIntercepts
{
    /// <summary>
    /// The exception context.
    /// </summary>
    public class ExceptionContext
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>
        /// The context.
        /// </value>
        public HttpContext Context { get; private set; }

        /// <summary>
        /// Gets the exception.
        /// </summary>
        /// <value>
        /// The exception.
        /// </value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ExceptionContext"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="exception">The exception.</param>
        internal ExceptionContext(HttpContext context, Exception exception)
        {
            Context = context;
            Exception = exception;
        }
    }
}
