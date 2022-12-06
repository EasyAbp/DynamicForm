using System;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.BookRentals;

public class BookRentalFormItem : Entity, IFormItemMetadata
{
    public Guid BookRentalId { get; set; }

    public string Name { get; set; } = null!;

    public string Group { get; set; }

    public string InfoText { get; set; }

    public string Type { get; set; } = null!;

    public bool Optional { get; set; }

    public string Configurations { get; set; }

    public int DisplayOrder { get; set; }

    public AvailableValues AvailableValues { get; set; }

    public override object[] GetKeys()
    {
        return new object[] { BookRentalId, Name };
    }
}