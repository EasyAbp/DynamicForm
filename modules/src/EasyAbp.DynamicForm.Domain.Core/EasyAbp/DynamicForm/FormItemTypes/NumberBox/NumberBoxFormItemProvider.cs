using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes.NumberBox;

public class NumberBoxFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "NumberBox";
    public static string LocalizationItemKey { get; set; } = "FormItemType.NumberBox";

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        var configurations = GetConfigurations<NumberBoxFormItemConfigurations>(metadata);

        if (configurations.MinValue.HasValue && configurations.MaxValue.HasValue &&
            configurations.MinValue > configurations.MaxValue)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.NumberBoxInvalidMaxValue)
                .WithData("item", metadata.Name);
        }

        if (metadata.AvailableValues.Any(x => !decimal.TryParse(x, out _)))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task ValidateValueAsync(IFormItemMetadata metadata, string value)
    {
        var isEmptyValue = value.IsNullOrWhiteSpace();

        if (!metadata.Optional && isEmptyValue)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired)
                .WithData("item", metadata.Name);
        }

        if (isEmptyValue)
        {
            return Task.CompletedTask;
        }

        if (!decimal.TryParse(value, out var numericValue))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.NumberBoxInvalidNumericValue)
                .WithData("item", metadata.Name);
        }

        var configurations = GetConfigurations<NumberBoxFormItemConfigurations>(metadata);

        int decimalPlaces = BitConverter.GetBytes(decimal.GetBits(numericValue)[3])[2];

        if (configurations.DecimalPlaces < decimalPlaces ||
            configurations.MinValue.HasValue && numericValue < configurations.MinValue ||
            (configurations.MaxValue.HasValue && numericValue > configurations.MaxValue))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.NumberBoxInvalidValue)
                .WithData("item", metadata.Name)
                .WithData("decimalPlaces", configurations.DecimalPlaces)
                .WithData("min", configurations.MinValue.HasValue ? configurations.MinValue.Value : "-∞")
                .WithData("max", configurations.MaxValue.HasValue ? configurations.MaxValue.Value : "∞");
        }

        if (metadata.AvailableValues.Any() && !metadata.AvailableValues.Select(decimal.Parse).Contains(numericValue))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new NumberBoxFormItemConfigurations());
    }
}