using System;
using EasyAbp.DynamicForm.Shared;
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

    public override object[] GetKeys()
    {
        return new object[] { FormTemplateId, Name };
    }
}