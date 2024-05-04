using PricePush.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace PricePush.Web.Pages
{
    /* Inherit your PageModel classes from this class.
     */
    public abstract class PricePushPageModel : AbpPageModel
    {
        protected PricePushPageModel()
        {
            LocalizationResourceType = typeof(PricePushResource);
        }
    }
}