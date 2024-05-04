using Xunit;

namespace PricePush.EntityFrameworkCore
{
    [CollectionDefinition(PricePushTestConsts.CollectionDefinitionName)]
    public class PricePushEntityFrameworkCoreCollection : ICollectionFixture<PricePushEntityFrameworkCoreFixture>
    {

    }
}
