using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace PricePush.Web
{
    [Dependency(ReplaceServices = true)]
    public class PricePushBrandingProvider : DefaultBrandingProvider
    {
        public override string AppName => "PricePush";
    }
}
