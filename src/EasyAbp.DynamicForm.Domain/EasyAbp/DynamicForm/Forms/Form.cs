using System;
using System.Collections.Generic;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Options;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.DynamicForm.Forms;

public class Form : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// Name of the <see cref="FormDefinition"/>.
    /// </summary>
    [NotNull]
    public virtual string FormDefinitionName { get; protected set; }

    /// <summary>
    /// The form template Id.
    /// </summary>
    public virtual Guid FormTemplateId { get; protected set; }

    /// <summary>
    /// Name of the form template.
    /// </summary>
    [NotNull]
    public virtual string FormTemplateName { get; protected set; }

    /// <summary>
    /// Form item list
    /// </summary>
    public virtual List<FormItem> FormItems { get; protected set; }

    protected Form()
    {
    }

    internal Form(Guid id, Guid? tenantId, [NotNull] string formDefinitionName, Guid formTemplateId,
        [NotNull] string formTemplateName, List<FormItem> formItems = null) : base(id)
    {
        TenantId = tenantId;
        FormDefinitionName = Check.NotNullOrWhiteSpace(formDefinitionName, nameof(formDefinitionName));
        FormTemplateId = formTemplateId;
        FormTemplateName = Check.NotNullOrWhiteSpace(formTemplateName, nameof(formTemplateName));
        FormItems = formItems ?? new List<FormItem>();
    }

    public void AddOrUpdateFormItem([NotNull] string name, FormItemType type, [CanBeNull] string value)
    {
        var item = FormItems.Find(x => x.Name == name);

        if (item is null)
        {
            item = new FormItem(Id, name, type, value);
            FormItems.Add(item);
        }
        else
        {
            item.Update(value);
        }
    }

    public FormItem FindFormItem([NotNull] string name) => FormItems.Find(x => x.Name == name);

    public FormItem GetFormItem([NotNull] string name) =>
        FindFormItem(name) ?? throw new EntityNotFoundException(typeof(FormItem), new { Id, name });

    public void RemoveFormItem([NotNull] string name) => FormItems.Remove(GetFormItem(name));
}