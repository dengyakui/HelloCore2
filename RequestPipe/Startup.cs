using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

namespace RequestPipe
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRouting();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            // regist a route
            app.UseRouter(builder => builder.MapGet("/action", async context => await context.Response.WriteAsync("this is a action")));


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.Map("/task", applicationBuilder => { applicationBuilder.Run(async context => { await context.Response.WriteAsync("tasks"); }); });
            app.Use(next =>
            {
                return (context) =>
                {
                    context.Response.WriteAsync("111\n");
                    return next.Invoke(context);
                };
            });

            app.Use(async (context, next) =>
            {
                await context.Response.WriteAsync("222\n");
                //await next.Invoke();
            });

            

            app.Run(async (context) => { await context.Response.WriteAsync("Hello World!"); });
        }
    }
}