using PricePush.EntityFrameworkCore;
using Volo.Abp.Modularity;

namespace PricePush
{
    [DependsOn(
        typeof(PricePushEntityFrameworkCoreTestModule)
        )]
    public class PricePushDomainTestModule : AbpModule
    {

    }
}