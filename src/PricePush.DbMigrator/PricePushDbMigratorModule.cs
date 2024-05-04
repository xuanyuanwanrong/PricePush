using PricePush.EntityFrameworkCore;
using Volo.Abp.Autofac;
using Volo.Abp.BackgroundJobs;
using Volo.Abp.Modularity;

namespace PricePush.DbMigrator
{
    [DependsOn(
        typeof(AbpAutofacModule),
        typeof(PricePushEntityFrameworkCoreModule),
        typeof(PricePushApplicationContractsModule)
        )]
    public class PricePushDbMigratorModule : AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<AbpBackgroundJobOptions>(options => options.IsJobExecutionEnabled = false);
        }
    }
}
