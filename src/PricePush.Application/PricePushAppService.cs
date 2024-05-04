using System;
using System.Collections.Generic;
using System.Text;
using PricePush.Localization;
using Volo.Abp.Application.Services;

namespace PricePush
{
    /* Inherit your application services from this class.
     */
    public abstract class PricePushAppService : ApplicationService
    {
        protected PricePushAppService()
        {
            LocalizationResource = typeof(PricePushResource);
        }
    }
}
