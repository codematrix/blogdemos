// Copyright (c) .NET Foundation. All rights reserved. 
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information. 

using Microsoft.AspNet.Diagnostics.ExceptionIntercepts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Microsoft.AspNet.Builder
{
    /// <summary>
    /// Exception Intercept Extensions.
    /// </summary>
    public static class ExceptionInterceptExtensions
    {
        /// <summary>
        /// Adds the exception intercepts.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns></returns>
        public static IServiceCollection AddExceptionIntercepts(this IServiceCollection services)
        {
            services.TryAddSingleton<IExceptionInterceptManager, ExceptionInterceptManager>();
            return services;
        }

        /// <summary>
        /// Uses the exception intercepts.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseExceptionIntercepts(this IApplicationBuilder builder)
        {
            var exceptionManager = builder.ApplicationServices.GetService<IExceptionInterceptManager>();
            if (exceptionManager != null)
            {
                builder.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(exceptionManager.InterceptAsync);
                });
            }

            return builder;
        }

        /// <summary>
        /// Adds the exception intercept.
        /// </summary>
        /// <param name="builder">The builder.</param>
        /// <param name="exceptionIntercept">The exception intercept.</param>
        /// <returns></returns>
        public static IApplicationBuilder AddExceptionIntercept(this IApplicationBuilder builder, IExceptionHandler exceptionInspector)
        {
            var exceptionManager = builder.ApplicationServices.GetService<IExceptionInterceptManager>();
            if (exceptionManager != null)
            {
                exceptionManager.AddExceptionIntercept(exceptionInspector);
            }

            return builder;
        }
    }
}
