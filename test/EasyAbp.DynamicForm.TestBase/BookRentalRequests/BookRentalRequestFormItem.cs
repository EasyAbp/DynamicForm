using System;
using EasyAbp.DynamicForm.Forms;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.BookRentalRequests;

public class BookRentalRequestFormItem : Entity, IFormItem
{
    public Guid BookRentalRequestId { get; set; }

    public string Name { get; set; }

    public string Type { get; set; }

    public string Value { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BookRentalRequestId, Name };
    }
}