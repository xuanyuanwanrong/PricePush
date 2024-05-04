using PricePush.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace PricePush.Permissions
{
    public class PricePushPermissionDefinitionProvider : PermissionDefinitionProvider
    {
        public override void Define(IPermissionDefinitionContext context)
        {
            var myGroup = context.AddGroup(PricePushPermissions.GroupName);
            //Define your own permissions here. Example:
            //myGroup.AddPermission(PricePushPermissions.MyPermission1, L("Permission:MyPermission1"));
        }

        private static LocalizableString L(string name)
        {
            return LocalizableString.Create<PricePushResource>(name);
        }
    }
}
