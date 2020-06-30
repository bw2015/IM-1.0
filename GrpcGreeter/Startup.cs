﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GrpcGreeter.Utils;
using IM.Model.DbSetting;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SP.StudioCore.Data.Repository;
using SP.StudioCore.Gateway.Push;
using SP.StudioCore.Ioc;
using SP.StudioCore.Services;

namespace GrpcGreeter
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IWriteRepository, WriteExecutor>()
                .AddTransient<IReadRepository, ReadExecutor>()
                .AddSingleton<IPush, Pusher>()
                .Initialize()
                .AddService();

            services.AddGrpc(opt =>
            {
                opt.Interceptors.Add<GrpcAuthorizationInterceptors>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            //app.UseAuthentication();
            //app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<GreeterService>();
            });
        }
    }
}
