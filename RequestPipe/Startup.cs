﻿using System;
using System.Collections.Generic;
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouter(r => r.MapGet("/action", context => context.Response.WriteAsync("this is an action")));


            var routeHandler = new RouteHandler(context => context.Response.WriteAsync("this is an task"));

            app.UseRouter(new Route(routeHandler,"/task",app.ApplicationServices.GetService<IInlineConstraintResolver>()));

            app.Run(context => context.Response.WriteAsync("end..."));


        }
    }
}