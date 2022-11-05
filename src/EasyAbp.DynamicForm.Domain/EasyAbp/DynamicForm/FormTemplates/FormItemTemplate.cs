using System;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormItemTemplate : Entity, IFormItemTemplate
{
    public virtual Guid FormTemplateId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual string Tip { get; protected set; }

    public virtual FormItemType Type { get; protected set; }

    public virtual bool Optional { get; protected set; }

    public virtual FormItemTemplateRadioValues RadioValues { get; protected set; }

    public FormItemTemplate()
    {
    }

    internal FormItemTemplate(
        Guid formTemplateId,
        [NotNull] string name,
        [CanBeNull] string tip,
        FormItemType type,
        bool optional,
        FormItemTemplateRadioValues radioValues)
    {
        FormTemplateId = formTemplateId;
        Name = name;
        Tip = tip;
        Type = type;
        Optional = optional;
        RadioValues = radioValues;
    }

    internal void Update(
        [CanBeNull] string tip,
        FormItemType type,
        bool optional,
        FormItemTemplateRadioValues radioValues)
    {
        Tip = tip;
        Type = type;
        Optional = optional;
        RadioValues = radioValues ?? new FormItemTemplateRadioValues();
    }

    public override object[] GetKeys()
    {
        return new object[] { FormTemplateId, Name };
    }
}