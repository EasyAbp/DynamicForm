using System;
using System.Collections.Generic;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Options;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplate : FullAuditedAggregateRoot<Guid>, IMultiTenant
{
    public virtual Guid? TenantId { get; protected set; }

    /// <summary>
    /// Name of the <see cref="FormDefinition"/>.
    /// </summary>
    [NotNull]
    public virtual string FormDefinitionName { get; protected set; }

    /// <summary>
    /// Form name.
    /// </summary>
    [NotNull]
    public virtual string Name { get; protected set; }

    /// <summary>
    /// A custom tag. It can be used for auth and filter.
    /// </summary>
    /// <example>
    /// {ProcessId}+{OrganizationUnitId}
    /// </example>
    [CanBeNull]
    public virtual string CustomTag { get; protected set; }

    /// <summary>
    /// Form item template list
    /// </summary>
    public virtual List<FormItemTemplate> FormItemTemplates { get; protected set; }

    protected FormTemplate()
    {
    }

    public FormTemplate(
        Guid id,
        Guid? tenantId,
        [NotNull] string formDefinitionName,
        [NotNull] string name,
        [CanBeNull] string customTag,
        List<FormItemTemplate> formItemTemplates) : base(id)
    {
        TenantId = tenantId;
        FormDefinitionName = formDefinitionName;
        Name = name;
        CustomTag = customTag;
        FormItemTemplates = formItemTemplates;
    }

    public void AddOrUpdateFormItemTemplate(
        [NotNull] string name,
        [CanBeNull] string tip,
        FormItemType type,
        bool optional,
        FormItemTemplateRadioValues radioValues)
    {
        var item = FormItemTemplates.Find(x => x.Name == name);

        if (item is null)
        {
            item = new FormItemTemplate(Id, name, tip, type, optional, radioValues);
            FormItemTemplates.Add(item);
        }
        else
        {
            item.Update(tip, type, optional, radioValues);
        }
    }

    public FormItemTemplate FindFormItemTemplate([NotNull] string name) => FormItemTemplates.Find(x => x.Name == name);

    public FormItemTemplate GetFormItemTemplate([NotNull] string name) =>
        FindFormItemTemplate(name) ?? throw new EntityNotFoundException(typeof(FormItemTemplate), new { Id, name });

    public void RemoveFormItemTemplate([NotNull] string name) => FormItemTemplates.Remove(GetFormItemTemplate(name));
}