using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormItemTypes.TextBox;

public class TextBoxFormItemProvider : IFormItemProvider, IScopedDependency
{
    public static string Name { get; set; } = "TextBox";
    public static string LocalizationItemKey { get; set; } = "FormItemType.TextBox";

    private readonly IJsonSerializer _jsonSerializer;

    public TextBoxFormItemProvider(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual Task ValidateTemplateAsync(IFormItemTemplate formItemTemplate)
    {
        var configurations = GetConfigurations(formItemTemplate);

        if (configurations.MinLength.HasValue && configurations.MaxLength.HasValue &&
            configurations.MinLength > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidMaxLength);
        }

        if (!configurations.RegexPattern.IsNullOrWhiteSpace())
        {
            try
            {
                var _ = Regex.Match(string.Empty, configurations.RegexPattern!);
            }
            catch
            {
                throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidRegexPattern);
            }
        }

        return Task.CompletedTask;
    }

    public virtual Task ValidateValueAsync(IFormItemMetadata formItemMetadata, string value)
    {
        if (!formItemMetadata.Optional && value.IsNullOrWhiteSpace())
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.FormItemValueIsRequired);
        }

        if (formItemMetadata.AvailableValues.Any() && !formItemMetadata.AvailableValues.Contains(value))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        var configurations = GetConfigurations(formItemMetadata);

        if (configurations.MinLength.HasValue && value?.Length < configurations.MinLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidValueLength);
        }

        if (configurations.MaxLength.HasValue && value?.Length > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.TextBoxInvalidValueLength);
        }

        if (!value.IsNullOrWhiteSpace() && !configurations.RegexPattern.IsNullOrWhiteSpace() &&
            !Regex.IsMatch(value!, configurations.RegexPattern!))
        {
            throw new BusinessException(DynamicFormCoreErrorCodes.InvalidFormItemValue);
        }

        return Task.CompletedTask;
    }

    public virtual Task<object> CreateConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new TextBoxFormItemConfigurations());
    }

    protected virtual TextBoxFormItemConfigurations GetConfigurations(IFormItemMetadata formItemTemplate)
    {
        return _jsonSerializer.Deserialize<TextBoxFormItemConfigurations>(formItemTemplate.Configurations);
    }
}