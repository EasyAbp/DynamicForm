using System;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormItemTemplate : Entity, IFormItemTemplate
{
    public virtual Guid FormTemplateId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual string InfoText { get; protected set; }

    public virtual string Type { get; protected set; }

    public virtual bool Optional { get; protected set; }

    public virtual string Configurations { get; protected set; }

    public virtual AvailableValues AvailableValues { get; protected set; }

    public virtual int DisplayOrder { get; protected set; }

    protected FormItemTemplate()
    {
    }

    internal FormItemTemplate(
        Guid formTemplateId,
        [NotNull] string name,
        [CanBeNull] string infoText,
        [NotNull] string type,
        bool optional,
        [CanBeNull] string configurations,
        AvailableValues availableValues,
        int displayOrder)
    {
        FormTemplateId = formTemplateId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        InfoText = infoText;
        Type = Check.NotNullOrWhiteSpace(type, nameof(type));
        Optional = optional;
        Configurations = configurations;
        AvailableValues = availableValues ?? new AvailableValues();
        DisplayOrder = displayOrder;
    }

    internal void Update(
        [CanBeNull] string infoText,
        [NotNull] string type,
        bool optional,
        [CanBeNull] string configurations,
        AvailableValues availableValues,
        int displayOrder)
    {
        InfoText = infoText;
        Type = Check.NotNullOrWhiteSpace(type, nameof(type));
        Optional = optional;
        Configurations = configurations;
        AvailableValues = availableValues ?? new AvailableValues();
        DisplayOrder = displayOrder;
    }

    public override object[] GetKeys()
    {
        return new object[] { FormTemplateId, Name };
    }
}