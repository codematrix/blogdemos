using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.PlatformAbstractions;
using ExceptionIntercepts.BasicSamples;
using ExceptionIntercepts.RealisticSamples;

namespace ExceptionIntercepts
{
    public class Startup
    {
        public Startup(IHostingEnvironment env, IApplicationEnvironment appEnv)
        {

        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
            services.AddExceptionIntercepts();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            // NOTE: the order of configuration is important as Middlewares are based on
            // a sequential Pipeline workflow.  

            // *** Simple examples - the order of addition is important as each interceptor is executed in sequence.
            //app.UseExceptionIntercepts();
            //app.AddExceptionIntercept(new SimpleExceptionInspector());
            //app.AddExceptionIntercept(new SimpleExceptionLogger1(loggerFactory));
            //app.AddExceptionIntercept(new SimpleExceptionLogger2(loggerFactory));
            //app.AddExceptionIntercept(new SimpleExceptionFinalizer());


            // *** Realistic examples - the order of addition is important as each interceptor is executed in sequence.
            app.UseExceptionIntercepts();
            app.AddExceptionIntercept(new ExceptionInspector(new ExceptionCategorizer()));
            app.AddExceptionIntercept(new ExceptionJIRALogger());
            app.AddExceptionIntercept(new ExceptionDbLogger());
            app.AddExceptionIntercept(new ExceptionFinalizer());

            app.UseIISPlatformHandler();
            app.UseMvc();
        }

        // Entry point for the application.
        public static void Main(string[] args) => WebApplication.Run<Startup>(args);
    }
}
