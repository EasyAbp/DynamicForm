using Volo.Abp.Reflection;

namespace EasyAbp.DynamicForm.Permissions;

public class DynamicFormPermissions
{
    public const string GroupName = "EasyAbp.DynamicForm";

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(DynamicFormPermissions));
    }

    public class Form
    {
        public const string Default = GroupName + ".Form";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }

    public class FormTemplate
    {
        public const string Default = GroupName + ".FormTemplate";
        public const string Update = Default + ".Update";
        public const string Create = Default + ".Create";
        public const string Delete = Default + ".Delete";
    }
}
