using EasyAbp.DynamicForm.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace EasyAbp.DynamicForm.Permissions;

public class DynamicFormPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(DynamicFormPermissions.GroupName, L("Permission:DynamicForm"));

        var formPermission = myGroup.AddPermission(DynamicFormPermissions.Form.Default, L("Permission:Form"));
        formPermission.AddChild(DynamicFormPermissions.Form.Create, L("Permission:Create"));
        formPermission.AddChild(DynamicFormPermissions.Form.Update, L("Permission:Update"));
        formPermission.AddChild(DynamicFormPermissions.Form.Delete, L("Permission:Delete"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<DynamicFormResource>(name);
    }
}
