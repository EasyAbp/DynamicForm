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

    public virtual FormItemType Type { get; protected set; }

    public virtual bool Optional { get; protected set; }
    
    public virtual AvailableRadioValues RadioValues { get; protected set; }

    public virtual string Value { get; protected set; }

    protected FormItem()
    {
    }

    internal FormItem(
        Guid formId,
        [NotNull] string name,
        FormItemType type,
        bool optional,
        AvailableRadioValues radioValues,
        [CanBeNull] string value)
    {
        FormId = formId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Type = type;
        Optional = optional;
        RadioValues = radioValues;
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