using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Services;

namespace EasyAbp.DynamicForm.Forms;

public class FormManager : DomainService
{
    protected IFormRepository FormRepository { get; }

    public FormManager(IFormRepository formRepository)
    {
        FormRepository = formRepository;
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

        await ValidateFormItemValueAsync(metadata, value);

        form.CreateFormItem(name, metadata, value);

        return form;
    }

    public virtual async Task<Form> UpdateFormItemAsync(Form form, [NotNull] string name, [CanBeNull] string value)
    {
        var item = form.GetFormItem(name);

        await ValidateFormItemValueAsync(item, value);

        form.UpdateFormItem(item, value);

        return form;
    }

    public virtual Task<Form> DeleteFormItemAsync(Form form, [NotNull] string name)
    {
        var item = form.GetFormItem(name);

        form.RemoveFormItem(item);

        return Task.FromResult(form);
    }

    protected virtual Task ValidateFormItemValueAsync(IFormItemMetadata metadata, [CanBeNull] string value)
    {
        switch (metadata.Type)
        {
            case FormItemType.Text:
                break;
            case FormItemType.Radio:
                if (!metadata.RadioValues.Contains(value))
                {
                    throw new BusinessException(DynamicFormErrorCodes.InvalidFormItemValue);
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        return Task.CompletedTask;
    }
}