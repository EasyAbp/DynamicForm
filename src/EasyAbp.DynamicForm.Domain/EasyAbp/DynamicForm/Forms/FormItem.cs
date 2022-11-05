using System;
using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;
using Volo.Abp;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.Forms;

public class FormItem : Entity, IFormItem
{
    public virtual Guid FormId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual FormItemType Type { get; protected set; }

    public virtual string Value { get; protected set; }

    protected FormItem()
    {
    }

    internal FormItem(
        Guid formId,
        [NotNull] string name,
        FormItemType type,
        [CanBeNull] string value)
    {
        FormId = formId;
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Type = type;
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