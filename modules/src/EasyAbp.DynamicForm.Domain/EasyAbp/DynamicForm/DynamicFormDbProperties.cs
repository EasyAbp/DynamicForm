namespace EasyAbp.DynamicForm;

public static class DynamicFormDbProperties
{
    public static string DbTablePrefix { get; set; } = "EasyAbpDynamicForm";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "EasyAbpDynamicForm";
}
