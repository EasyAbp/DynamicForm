using System;
using System.Collections.Generic;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Options;
using JetBrains.Annotations;
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
}