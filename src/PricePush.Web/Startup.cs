using Autofac.Core;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PricePush.BackgroundJobs.Telegram;
using System;

namespace PricePush.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            var configuration = services.GetConfiguration();
            services.AddApplication<PricePushWebModule>();
            services.AddHangfire(r => r.UseSqlServerStorage(configuration.GetConnectionString("Default")));
            services.AddHangfireServer();

        }

        public void Configure(IApplicationBuilder app)
        {
            app.InitializeApplication();

            app.UseHangfireDashboard(); //打开仪表盘
            BackgroundJob.Enqueue<TelegramBotMonitorJob>(x => x.Execute(new TelegramBotMonitorJobArgs
            {
                BotId = 0
            }));
        }
    }
}
