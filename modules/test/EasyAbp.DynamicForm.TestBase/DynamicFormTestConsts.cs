using System;

namespace EasyAbp.DynamicForm;

public static class DynamicFormTestConsts
{
    public static Guid User1Id { get; } = Guid.NewGuid();

    public static string TestFormDefinitionName => "InternalForm";

    public static string TestFormDefinitionDisplayName => "Internal Form";

    public static Guid FormTemplate1Id { get; } = Guid.NewGuid();

    public static string FormTemplate1Name => "Leave Application";
}