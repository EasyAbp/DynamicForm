using System;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.Forms;

public class FormItem : Entity, IFormItem, IFormItemMetadata
{
    public virtual Guid FormId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual string Group { get; protected set; }

    public virtual string InfoText { get; protected set; }

    public virtual string Type { get; protected set; }

    public virtual bool Optional { get; protected set; }

    public virtual string Configurations { get; protected set; }

    public virtual AvailableValues AvailableValues { get; protected set; }

    public virtual int DisplayOrder { get; protected set; }

    public virtual string Value { get; protected set; }

    protected FormItem()
    {
    }

    internal FormItem(
        Guid formId,
        [NotNull] string name,
        [CanBeNull] string group,
        [CanBeNull] string infoText,
        [NotNull] string type,
        bool optional,
        [CanBeNull] string configurations,
        AvailableValues availableValues,
        int displayOrder,
        [CanBeNull] string value)
    {
        FormId = formId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Group = group;
        InfoText = infoText;
        Type = Check.NotNullOrWhiteSpace(type, nameof(type));
        Optional = optional;
        Configurations = configurations;
        AvailableValues = availableValues;
        DisplayOrder = displayOrder;
        Value = value;
    }

    internal void Update([CanBeNull] string value)
    {
        Value = value;
    }

    public override object[] GetKeys()
    {
        return new object[] { FormId, Name };
    }
}