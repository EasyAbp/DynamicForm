using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormItemTemplate : IFormItemMetadata
{
    [CanBeNull]
    string Tip { get; }
}