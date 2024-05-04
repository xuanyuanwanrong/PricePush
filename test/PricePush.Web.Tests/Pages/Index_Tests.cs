using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace PricePush.Pages
{
    [Collection(PricePushTestConsts.CollectionDefinitionName)]
    public class Index_Tests : PricePushWebTestBase
    {
        [Fact]
        public async Task Welcome_Page()
        {
            var response = await GetResponseAsStringAsync("/");
            response.ShouldNotBeNull();
        }
    }
}
