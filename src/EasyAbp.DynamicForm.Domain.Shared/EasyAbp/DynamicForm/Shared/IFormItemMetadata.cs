using EasyAbp.DynamicForm.FormTemplates;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Shared;

public interface IFormItemMetadata
{
    [NotNull]
    string Name { get; }

    FormItemType Type { get; }

    /// <remarks>
    /// If it's set to <c>true</c>, a string form item can't be <c>null</c>, empty, or white space.
    /// </remarks>
    bool Optional { get; }

    /// <remarks>
    /// Available radio values. It only affects when the from item type is `Radio`.
    /// </remarks>
    AvailableRadioValues RadioValues { get; }
}