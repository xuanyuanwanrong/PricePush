using Autofac.Core;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PricePush.BackgroundJobs.Telegram;
using PricePush.Database;
using PricePush.Dtos.Appsetting;
using PricePush.Helpr.Cache;
using System;
using System.Configuration;

namespace PricePush.Web
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            var configuration = services.GetConfiguration();
            services.AddApplication<PricePushWebModule>();
            services.Configure<RedisSetting>(configuration.GetSection("Redis"));
            services.Configure<DatabaseSetting>(configuration.GetSection("ConnectionStrings"));
            services.AddHangfire(r => r.UseSqlServerStorage(configuration.GetConnectionString("Default")));
            services.AddHangfireServer();

        }

        public void Configure(IApplicationBuilder app, IOptions<RedisSetting> redis, IOptions<DatabaseSetting> database)
        {

            new RedisCache($"{redis.Value.Server}:{redis.Value.Port}", redis.Value.Database);

            new Sqlsugar(database.Value.Default);
            app.InitializeApplication();

            app.UseHangfireDashboard(); //打开仪表盘
            BackgroundJob.Enqueue<TelegramBotMonitorJob>(x => x.Execute(new TelegramBotMonitorJobArgs
            {
                BotId = 0
            }));

        }
    }
}
