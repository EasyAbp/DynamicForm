using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Options;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateManager : DomainService
{
    protected DynamicFormOptions Options { get; }
    protected IFormTemplateRepository FormTemplateRepository { get; }

    public FormTemplateManager(
        IOptions<DynamicFormOptions> options,
        IFormTemplateRepository formTemplateRepository)
    {
        Options = options.Value;
        FormTemplateRepository = formTemplateRepository;
    }

    public virtual async Task<FormTemplate> CreateAsync([NotNull] string formDefinitionName, [NotNull] string name,
        [CanBeNull] string customTag, List<FormItemTemplate> formItemTemplates = null)
    {
        await ValidateFormDefinitionNameAsync(formDefinitionName);

        return new FormTemplate(
            GuidGenerator.Create(), CurrentTenant.Id, formDefinitionName, name, customTag, formItemTemplates);
    }

    public virtual Task<FormTemplate> UpdateAsync(FormTemplate formTemplate, [NotNull] string name)
    {
        formTemplate.Update(name);

        return Task.FromResult(formTemplate);
    }

    public virtual Task<FormTemplate> CreateFormItemAsync(FormTemplate formTemplate, [NotNull] string name,
        [CanBeNull] string infoText, FormItemType type, bool optional, AvailableRadioValues radioValues)
    {
        var item = formTemplate.FindFormItemTemplate(name);

        if (item is not null)
        {
            throw new BusinessException(DynamicFormErrorCodes.DuplicateFormItemTemplate);
        }

        formTemplate.AddOrUpdateFormItemTemplate(name, infoText, type, optional, radioValues);

        return Task.FromResult(formTemplate);
    }

    public virtual Task<FormTemplate> UpdateFormItemAsync(FormTemplate formTemplate, [NotNull] string name,
        [CanBeNull] string infoText, FormItemType type, bool optional, AvailableRadioValues radioValues)
    {
        var item = formTemplate.GetFormItemTemplate(name);

        formTemplate.AddOrUpdateFormItemTemplate(name, infoText, type, optional, radioValues);

        return Task.FromResult(formTemplate);
    }

    public virtual Task<FormTemplate> RemoveFormItemAsync(FormTemplate formTemplate, [NotNull] string name)
    {
        formTemplate.RemoveFormItemTemplate(name);

        return Task.FromResult(formTemplate);
    }

    protected virtual Task ValidateFormDefinitionNameAsync([NotNull] string formDefinitionName)
    {
        Check.NotNullOrWhiteSpace(formDefinitionName, nameof(formDefinitionName));

        Options.GetFormDefinition(formDefinitionName);

        return Task.CompletedTask;
    }
}