using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Forms;

public interface IFormItem
{
    [NotNull]
    string Name { get; }

    [CanBeNull]
    string Value { get; }
}