using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace WcfServices
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR()
                .AddAzureSignalR(Environment.GetEnvironmentVariable("AzureSignalR_ConnectionString"));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IApplicationLifetime applicationLifetime)
        {
            app.UseAzureSignalR(routes =>
            {
                routes.MapHub<CustomerHub>("/customerhub");
            });
        }
    }
}