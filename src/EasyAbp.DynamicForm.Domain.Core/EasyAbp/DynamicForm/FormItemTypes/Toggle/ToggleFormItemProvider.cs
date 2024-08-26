using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes.Toggle;

public class ToggleFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "Toggle";
    public static string LocalizationItemKey { get; set; } = "FormItemType.Toggle";

    public static string TrueValue { get; set; } = true.ToString();
    public static string FalseValue { get; set; } = false.ToString();

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        if (metadata.Optional)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.ToggleIsOptional)
                .WithData("item", metadata.Name);
        }

        if (metadata.AvailableValues.Any(x =>
                !x.Equals(TrueValue, StringComparison.InvariantCultureIgnoreCase) &&
                !x.Equals(FalseValue, StringComparison.InvariantCultureIgnoreCase)))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        var isEmptyValue = value.IsNullOrWhiteSpace();

        if (isEmptyValue)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired)
                .WithData("item", metadata.Name);
        }

        if (metadata.AvailableValues.Any() &&
            !metadata.AvailableValues.Contains(value, StringComparer.InvariantCultureIgnoreCase))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        if (value.Equals(TrueValue, StringComparison.InvariantCultureIgnoreCase) &&
            value.Equals(FalseValue, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new ToggleFormItemConfigurations());
    }
}