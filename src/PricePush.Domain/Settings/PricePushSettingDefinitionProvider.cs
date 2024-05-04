using Volo.Abp.Settings;

namespace PricePush.Settings
{
    public class PricePushSettingDefinitionProvider : SettingDefinitionProvider
    {
        public override void Define(ISettingDefinitionContext context)
        {
            //Define your own settings here. Example:
            //context.Add(new SettingDefinition(PricePushSettings.MySetting1));
        }
    }
}
