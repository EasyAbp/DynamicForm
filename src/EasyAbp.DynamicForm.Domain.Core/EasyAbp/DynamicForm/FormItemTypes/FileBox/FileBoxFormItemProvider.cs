using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.FileBox;

public class FileBoxFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "FileBox";
    public static string LocalizationItemKey { get; set; } = "FormItemType.FileBox";

    protected IEnumerable<IFileBoxValueValidator> ValueValidators { get; }

    public FileBoxFormItemProvider(
        IJsonSerializer jsonSerializer,
        IEnumerable<IFileBoxValueValidator> valueValidators) : base(jsonSerializer)
    {
        ValueValidators = valueValidators;
    }

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        return Task.CompletedTask;
    }

    public override async Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        foreach (var valueValidator in ValueValidators)
        {
            await valueValidator.ValidateAsync(metadata, value);
        }
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new FileBoxFormItemConfigurations());
    }
}