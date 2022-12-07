using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes.FileBox;

public class FileBoxFormItemConfigurations
{
    [NotNull]
    public string ProviderName { get; set; } = null!;

    [CanBeNull]
    public string ProviderKey { get; set; }
}