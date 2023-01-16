using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormItemTemplate : IFormItemMetadata
{
    bool Disabled { get; }
}