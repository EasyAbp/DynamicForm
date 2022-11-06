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

    public virtual FormItemType Type { get; protected set; }

    public virtual bool Optional { get; protected set; }

    public virtual AvailableRadioValues RadioValues { get; protected set; }

    public virtual int DisplayOrder { get; protected set; }

    protected FormItemTemplate()
    {
    }

    internal FormItemTemplate(
        Guid formTemplateId,
        [NotNull] string name,
        [CanBeNull] string infoText,
        FormItemType type,
        bool optional,
        AvailableRadioValues radioValues,
        int displayOrder)
    {
        FormTemplateId = formTemplateId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        InfoText = infoText;
        Type = type;
        Optional = optional;
        RadioValues = radioValues ?? new AvailableRadioValues();
        DisplayOrder = displayOrder;
    }

    internal void Update(
        [CanBeNull] string infoText,
        FormItemType type,
        bool optional,
        AvailableRadioValues radioValues,
        int displayOrder)
    {
        InfoText = infoText;
        Type = type;
        Optional = optional;
        RadioValues = radioValues ?? new AvailableRadioValues();
        DisplayOrder = displayOrder;
    }

    public override object[] GetKeys()
    {
        return new object[] { FormTemplateId, Name };
    }
}