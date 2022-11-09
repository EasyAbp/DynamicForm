using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormItemTemplate : IFormItemMetadata
{
    /// <summary>
    /// Info text (tips) for the form item.
    /// </summary>
    [CanBeNull]
    string InfoText { get; }
}