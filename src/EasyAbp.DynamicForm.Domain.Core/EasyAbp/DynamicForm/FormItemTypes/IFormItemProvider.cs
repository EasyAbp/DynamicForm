using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes;

public interface IFormItemProvider
{
    Task ValidateTemplateAsync(IFormItemTemplate formItemTemplate);

    Task ValidateValueAsync(IFormItemMetadata formItemMetadata, [CanBeNull] string value);

    [ItemCanBeNull]
    Task<object> CreateConfigurationsObjectOrNullAsync();
}