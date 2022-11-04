using EasyAbp.DynamicForm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.DynamicForm.Permissions;

public class DynamicFormPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DynamicFormPermissions.GroupName, L("Permission:DynamicForm"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DynamicFormResource>(name);
    }
}
