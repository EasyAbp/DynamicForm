using System;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.Forms;

public class FormItem : Entity, IFormItem, IFormItemMetadata
{
    public virtual Guid FormId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual string Type { get; protected set; }

    public virtual bool Optional { get; protected set; }

    public virtual string Configurations { get; protected set; }

    public virtual AvailableValues AvailableValues { get; protected set; }

    public virtual string Value { get; protected set; }

    protected FormItem()
    {
    }

    internal FormItem(
        Guid formId,
        [NotNull] string name,
        [NotNull] string type,
        bool optional,
        [CanBeNull] string configurations,
        AvailableValues values,
        [CanBeNull] string value)
    {
        FormId = formId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Type = Check.NotNullOrWhiteSpace(type, nameof(type));
        Optional = optional;
        Configurations = configurations;
        AvailableValues = values;
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