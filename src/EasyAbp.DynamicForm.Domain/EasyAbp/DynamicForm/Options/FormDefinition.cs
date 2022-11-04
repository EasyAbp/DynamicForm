using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Options;

public class FormDefinition
{
    [NotNull]
    public string Name { get; }

    [CanBeNull]
    public string DisplayName { get; }

    public FormDefinition([NotNull] string name, [CanBeNull] string displayName)
    {
        Name = name;
        DisplayName = displayName;
    }
}