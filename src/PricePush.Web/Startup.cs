using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace PricePush.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApplication<PricePushWebModule>();
            services.AddHangfire(r => r.UseSqlServerStorage("Server=101.43.44.78;Database=pricePush;User Id=pricePush;Password=zA(V6Quk%u)aE8ZR9;"));
        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();

            app.UseHangfireDashboard(); //打开仪表盘
        
        }
    }
}
