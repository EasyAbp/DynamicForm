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
    protected DynamicFormOptions DynamicFormOptions { get; }
    protected DynamicFormCoreOptions DynamicFormCoreOptions { get; }
    protected IDynamicFormValidator DynamicFormValidator { get; }
    protected IFormTemplateRepository FormTemplateRepository { get; }

    public FormTemplateManager(
        IOptions<DynamicFormOptions> dynamicFormOptions,
        IOptions<DynamicFormCoreOptions> dynamicFormCoreOptions,
        IDynamicFormValidator dynamicFormValidator,
        IFormTemplateRepository formTemplateRepository)
    {
        DynamicFormOptions = dynamicFormOptions.Value;
        DynamicFormCoreOptions = dynamicFormCoreOptions.Value;
        DynamicFormValidator = dynamicFormValidator;
        FormTemplateRepository = formTemplateRepository;
    }

    public virtual async Task<FormTemplate> CreateAsync([NotNull] string formDefinitionName, [NotNull] string name,
        [CanBeNull] string customTag)
    {
        await ValidateFormDefinitionNameAsync(formDefinitionName);

        return new FormTemplate(
            GuidGenerator.Create(), CurrentTenant.Id, formDefinitionName, name, customTag);
    }

    public virtual Task<FormTemplate> UpdateAsync(FormTemplate formTemplate, [NotNull] string name)
    {
        formTemplate.Update(name);

        return Task.FromResult(formTemplate);
    }

    public virtual async Task<FormTemplate> AddFormItemAsync(FormTemplate formTemplate, [NotNull] string name,
        [CanBeNull] string group, [CanBeNull] string infoText, [NotNull] string type, bool optional,
        [CanBeNull] string configurations, AvailableValues availableValues, int displayOrder, bool disabled)
    {
        formTemplate.AddOrUpdateFormItemTemplate(
            name, group, infoText, type, optional, configurations, availableValues, displayOrder, disabled);

        await DynamicFormValidator.ValidateTemplatesAsync(formTemplate.FormItemTemplates);

        return formTemplate;
    }

    public virtual async Task<FormTemplate> UpdateFormItemAsync(FormTemplate formTemplate, [NotNull] string name,
        [CanBeNull] string group, [CanBeNull] string infoText, [NotNull] string type, bool optional,
        string configurations, AvailableValues availableValues, int displayOrder, bool disabled)
    {
        formTemplate.GetFormItemTemplate(name);

        formTemplate.AddOrUpdateFormItemTemplate(
            name, group, infoText, type, optional, configurations, availableValues, displayOrder, disabled);

        await DynamicFormValidator.ValidateTemplatesAsync(formTemplate.FormItemTemplates);

        return formTemplate;
    }

    public virtual Task<FormTemplate> RemoveFormItemAsync(FormTemplate formTemplate, [NotNull] string name)
    {
        formTemplate.RemoveFormItemTemplate(name);

        return Task.FromResult(formTemplate);
    }

    protected virtual Task ValidateFormDefinitionNameAsync([NotNull] string formDefinitionName)
    {
        Check.NotNullOrWhiteSpace(formDefinitionName, nameof(formDefinitionName));

        DynamicFormOptions.GetFormDefinition(formDefinitionName);

        return Task.CompletedTask;
    }
}