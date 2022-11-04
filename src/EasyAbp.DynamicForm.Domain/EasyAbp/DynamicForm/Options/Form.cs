using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace EasyAbp.DynamicForm.Options;

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
}