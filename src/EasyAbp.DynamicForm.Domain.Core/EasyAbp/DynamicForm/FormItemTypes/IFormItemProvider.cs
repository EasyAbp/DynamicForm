using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes;

public interface IFormItemProvider
{
    Task ValidateTemplateAsync(IFormItemMetadata metadata);

    Task ValidateValueAsync(IFormItemMetadata metadata, [CanBeNull] string value);

    [ItemCanBeNull]
    Task<object> CreateConfigurationsObjectOrNullAsync();
}