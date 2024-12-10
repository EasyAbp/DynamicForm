using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.BookRentals;

public class BookRental : AggregateRoot<Guid>
{
    public string BookName { get; set; }

    public List<BookRentalFormItem> FormItems { get; set; } = new();

    protected BookRental()
    {
    }

    public BookRental(Guid id, string bookName) : base(id)
    {
        BookName = bookName;
    }
}