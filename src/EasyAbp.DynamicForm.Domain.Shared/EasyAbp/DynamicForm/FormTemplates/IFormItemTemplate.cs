using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormItemTemplate
{
    [NotNull]
    string Name { get; }

    [CanBeNull]
    string Tip { get; }

    FormItemType Type { get; }

    /// <remarks>
    /// If it's set to <c>true</c>, a string form item can't be <c>null</c>, empty, or white space.
    /// </remarks>
    bool Optional { get; }

    /// <remarks>
    /// Available radio values. It only affects when the from item type is `Radio`.
    /// </remarks>
    FormItemTemplateRadioValues RadioValues { get; }
}