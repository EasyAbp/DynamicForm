using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.FormItemTypes.TimePicker;

public class TimePickerFormItemProvider : FormItemProviderBase, IScopedDependency
{
    public static string Name { get; set; } = "TimePicker";
    public static string LocalizationItemKey { get; set; } = "FormItemType.TimePicker";

    public override Task ValidateTemplateAsync(IFormItemMetadata metadata)
    {
        if (metadata.AvailableValues.Any(x => !DateTime.TryParse(x, out _)))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TimePickerInvalidDateTime)
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


        if (!DateTime.TryParse(value, out var dateTimeValue))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TimePickerInvalidDateTime)
                .WithData("item", metadata.Name);
        }

        if (metadata.AvailableValues.Any() && !metadata.AvailableValues.Select(DateTime.Parse).Contains(dateTimeValue))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue)
                .WithData("item", metadata.Name);
        }

        return Task.CompletedTask;
    }

    public override Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new TimePickerFormItemConfigurations());
    }
}