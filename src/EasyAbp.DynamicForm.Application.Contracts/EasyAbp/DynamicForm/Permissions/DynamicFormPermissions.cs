using Volo.Abp.Reflection;

namespace EasyAbp.DynamicForm.Permissions;

public class DynamicFormPermissions
{
    public const string GroupName = "EasyAbp.DynamicForm";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DynamicFormPermissions));
    }
}
