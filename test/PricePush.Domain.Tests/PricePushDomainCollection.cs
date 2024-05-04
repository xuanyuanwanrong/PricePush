using PricePush.EntityFrameworkCore;
using Xunit;

namespace PricePush
{
    [CollectionDefinition(PricePushTestConsts.CollectionDefinitionName)]
    public class PricePushDomainCollection : PricePushEntityFrameworkCoreCollectionFixtureBase
    {

    }
}
