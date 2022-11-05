using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using JetBrains.Annotations;
using Volo.Abp.Domain.Services;

namespace EasyAbp.DynamicForm.Forms;

public class FormManager : DomainService
{
    protected IFormRepository FormRepository { get; }

    public FormManager(IFormRepository formRepository)
    {
        FormRepository = formRepository;
    }

    public virtual async Task<Form> CreateAsync(
        [NotNull] string formDefinitionName, FormTemplate formTemplate, List<FormItem> formItems = null)
    {
        formItems ??= new List<FormItem>();

        foreach (var formItem in formItems)
        {
            await ValidateFormItemAsync(formTemplate, formItem.Name, formItem.Value);
        }

        return new Form(
            GuidGenerator.Create(),
            CurrentTenant.Id,
            formDefinitionName,
            formTemplate.Id,
            formTemplate.Name,
            formItems);
    }

    public virtual async Task UpdateFormItemAsync(
        Form form, FormTemplate formTemplate, [NotNull] string name, [CanBeNull] string value)
    {
        await ValidateFormItemAsync(formTemplate, name, value);

        var item = form.GetFormItem(name);

        form.UpdateFormItem(item, value);
    }

    protected virtual Task ValidateFormItemAsync(
        FormTemplate formTemplate, [NotNull] string name, [CanBeNull] string value)
    {
        // Todo: validate form item.
        return Task.CompletedTask;
    }
}