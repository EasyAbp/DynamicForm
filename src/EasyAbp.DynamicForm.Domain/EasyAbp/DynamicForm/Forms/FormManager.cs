using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Options;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.DynamicForm.Forms;

public class FormManager : DomainService
{
    protected IFormRepository FormRepository { get; }
    protected DynamicFormOptions Options { get; }

    public FormManager(
        IFormRepository formRepository,
        IOptions<DynamicFormOptions> options)
    {
        FormRepository = formRepository;
        Options = options.Value;
    }

    public virtual Task<Form> CreateAsync(FormTemplate formTemplate)
    {
        return Task.FromResult(new Form(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            formTemplate.FormDefinitionName,
            formTemplate.Id,
            formTemplate.Name));
    }

    public virtual async Task<Form> CreateFormItemAsync(
        Form form, [NotNull] string name, IFormItemMetadata metadata, [CanBeNull] string value)
    {
        if (form.FindFormItem(name) is not null)
        {
            throw new BusinessException(DynamicFormErrorCodes.DuplicateFormItem);
        }

        await ValidateFormItemValueAsync(form, metadata, value);

        form.CreateFormItem(name, metadata, value);

        return form;
    }

    public virtual async Task<Form> UpdateFormItemAsync(Form form, [NotNull] string name, [CanBeNull] string value)
    {
        var item = form.GetFormItem(name);

        await ValidateFormItemValueAsync(form, item, value);

        form.UpdateFormItem(item, value);

        return form;
    }

    public virtual Task<Form> DeleteFormItemAsync(Form form, [NotNull] string name)
    {
        var item = form.GetFormItem(name);

        form.RemoveFormItem(item);

        return Task.FromResult(form);
    }

    protected virtual async Task ValidateFormItemValueAsync(Form form, IFormItemMetadata metadata,
        [CanBeNull] string value)
    {
        if (!metadata.Optional && value.IsNullOrWhiteSpace())
        {
            throw new BusinessException(DynamicFormErrorCodes.FormItemValueIsRequired);
        }

        if (metadata.AvailableValues.Any() && !metadata.AvailableValues.Contains(value))
        {
            throw new BusinessException(DynamicFormErrorCodes.InvalidFormItemValue);
        }

        var formItemProvider =
            (IFormItemProvider)LazyServiceProvider.LazyGetRequiredService(Options
                .GetFormItemTypeDefinition(metadata.Type).ProviderType);

        await formItemProvider.ValidateFormItemValueAsync(metadata, value);

        var formDefinition = Options.GetFormDefinition(form.FormDefinitionName);

        foreach (var validator in formDefinition.CustomFormItemValidatorTypes.Select(customValidatorType =>
                     (ICustomFormItemValidator)LazyServiceProvider.LazyGetRequiredService(customValidatorType)))
        {
            await validator.ValidateValueAsync(metadata, value);
        }
    }
}