using System;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.Options;

public class FormItem : Entity, IFormItem
{
    public virtual Guid FormId { get; protected set; }

    public virtual string Name { get; protected set; }

    public virtual FormItemType Type { get; protected set; }

    public virtual string Value { get; protected set; }

    public override object[] GetKeys()
    {
        return new object[] { FormId, Name };
    }
}