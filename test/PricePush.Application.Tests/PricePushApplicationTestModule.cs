using Volo.Abp.Modularity;

namespace PricePush
{
    [DependsOn(
        typeof(PricePushApplicationModule),
        typeof(PricePushDomainTestModule)
        )]
    public class PricePushApplicationTestModule : AbpModule
    {

    }
}