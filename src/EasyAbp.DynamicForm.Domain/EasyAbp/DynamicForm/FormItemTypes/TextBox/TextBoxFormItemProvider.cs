using System;
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
    public static string LocalizationItemKey { get; set; } = "TextBox";

    private readonly IJsonSerializer _jsonSerializer;

    public TextBoxFormItemProvider(IJsonSerializer jsonSerializer)
    {
        _jsonSerializer = jsonSerializer;
    }

    public virtual Task ValidateFormItemTemplateAsync(FormItemTemplate formItemTemplate)
    {
        var configurations = GetConfigurations(formItemTemplate);

        if (configurations.MinLength.HasValue && configurations.MaxLength.HasValue &&
            configurations.MinLength > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormErrorCodes.TextBoxInvalidMaxLength);
        }

        if (!configurations.RegexPattern.IsNullOrWhiteSpace())
        {
            try
            {
                var _ = Regex.Match(string.Empty, configurations.RegexPattern!);
            }
            catch
            {
                throw new BusinessException(DynamicFormErrorCodes.TextBoxInvalidRegexPattern);
            }
        }

        return Task.CompletedTask;
    }

    public virtual Task ValidateFormItemValueAsync(IFormItemMetadata formItemMetadata, string value)
    {
        var configurations = GetConfigurations(formItemMetadata);

        if (configurations.MinLength.HasValue && value?.Length < configurations.MinLength)
        {
            throw new BusinessException(DynamicFormErrorCodes.TextBoxInvalidValueLength);
        }

        if (configurations.MaxLength.HasValue && value?.Length > configurations.MaxLength)
        {
            throw new BusinessException(DynamicFormErrorCodes.TextBoxInvalidValueLength);
        }

        if (value.IsNullOrWhiteSpace() && configurations.RegexPattern.IsNullOrWhiteSpace() &&
            Regex.IsMatch(value!, configurations.RegexPattern!))
        {
            throw new BusinessException(DynamicFormErrorCodes.InvalidFormItemValue);
        }

        return Task.CompletedTask;
    }

    public virtual Task<object> CreateFormItemConfigurationsObjectOrNullAsync()
    {
        return Task.FromResult<object>(new TextBoxFormItemConfigurations());
    }

    protected virtual TextBoxFormItemConfigurations GetConfigurations(IFormItemMetadata formItemTemplate)
    {
        return _jsonSerializer.Deserialize<TextBoxFormItemConfigurations>(formItemTemplate.Configurations);
    }
}