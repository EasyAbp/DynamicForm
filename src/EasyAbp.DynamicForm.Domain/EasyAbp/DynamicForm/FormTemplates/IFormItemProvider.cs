using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormItemProvider
{
    Task ValidateFormItemTemplateAsync(FormItemTemplate formItemTemplate);

    Task ValidateFormItemValueAsync(IFormItemMetadata formItemMetadata, [CanBeNull] string value);

    [ItemCanBeNull]
    Task<object> CreateFormItemConfigurationsObjectOrNullAsync();
}