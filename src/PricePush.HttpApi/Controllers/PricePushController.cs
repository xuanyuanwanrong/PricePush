using PricePush.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace PricePush.Controllers
{
    /* Inherit your controllers from this class.
     */
    public abstract class PricePushController : AbpControllerBase
    {
        protected PricePushController()
        {
            LocalizationResource = typeof(PricePushResource);
        }
    }
}