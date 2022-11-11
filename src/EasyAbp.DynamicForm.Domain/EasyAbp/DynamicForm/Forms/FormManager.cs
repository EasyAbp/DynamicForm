using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Options;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Microsoft.Extensions.Options;
using Volo.Abp.Domain.Services;

namespace EasyAbp.DynamicForm.Forms;

public class FormManager : DomainService
{
    protected IFormRepository FormRepository { get; }
    protected DynamicFormOptions DynamicFormOptions { get; }
    protected DynamicFormCoreOptions DynamicFormCoreOptions { get; }
    protected IDynamicFormValidator DynamicFormValidator { get; }

    public FormManager(
        IFormRepository formRepository,
        IOptions<DynamicFormOptions> dynamicFormOptions,
        IOptions<DynamicFormCoreOptions> dynamicFormCoreOptions,
        IDynamicFormValidator dynamicFormValidator)
    {
        FormRepository = formRepository;
        DynamicFormOptions = dynamicFormOptions.Value;
        DynamicFormValidator = dynamicFormValidator;
        DynamicFormCoreOptions = dynamicFormCoreOptions.Value;
    }

    public virtual async Task<Form> CreateAsync(FormTemplate formTemplate, IEnumerable<IFormItem> formItems)
    {
        var listedFormItems = formItems.ToList();

        var form = new Form(GuidGenerator.Create(), CurrentTenant.Id, formTemplate.FormDefinitionName, formTemplate.Id,
            formTemplate.Name);

        await DynamicFormValidator.ValidateValuesAsync(formTemplate.FormItemTemplates, listedFormItems);

        foreach (var formItemTemplate in formTemplate.FormItemTemplates)
        {
            var formItem = listedFormItems.First(x => x.Name == formItemTemplate.Name);

            await AddFormItemAsync(form, formItemTemplate.Name, formItemTemplate, formItem.Value);
        }

        return form;
    }

    protected virtual async Task<Form> AddFormItemAsync(
        Form form, [NotNull] string name, IFormItemMetadata metadata, [CanBeNull] string value)
    {
        await CustomValidateFormItemValueAsync(form, metadata, value);

        form.CreateFormItem(name, metadata, value);

        return form;
    }

    public virtual async Task<Form> UpdateFormItemAsync(Form form, [NotNull] string name, [CanBeNull] string value)
    {
        var item = form.GetFormItem(name);

        await CustomValidateFormItemValueAsync(form, item, value);

        form.UpdateFormItem(item, value);

        return form;
    }

    public virtual Task<Form> DeleteFormItemAsync(Form form, [NotNull] string name)
    {
        var item = form.GetFormItem(name);

        form.RemoveFormItem(item);

        return Task.FromResult(form);
    }

    protected virtual async Task CustomValidateFormItemValueAsync(Form form, IFormItemMetadata metadata,
        [CanBeNull] string value)
    {
        var formDefinition = DynamicFormOptions.GetFormDefinition(form.FormDefinitionName);

        foreach (var validator in formDefinition.CustomFormItemValidatorTypes.Select(customValidatorType =>
                     (ICustomFormItemValidator)LazyServiceProvider.LazyGetRequiredService(customValidatorType)))
        {
            await validator.ValidateValueAsync(metadata, value);
        }
    }
}