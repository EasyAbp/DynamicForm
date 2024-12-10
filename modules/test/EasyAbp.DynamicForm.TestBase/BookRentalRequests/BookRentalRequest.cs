using System;
using System.Collections.Generic;
using Volo.Abp.Domain.Entities;

namespace EasyAbp.DynamicForm.BookRentalRequests;

public class BookRentalRequest : AggregateRoot<Guid>
{
    public Guid BookRentalId { get; set; }

    public Guid RenterUserId { get; set; }

    public List<BookRentalRequestFormItem> FormItems { get; set; } = new();

    protected BookRentalRequest()
    {
    }

    public BookRentalRequest(Guid id, Guid bookRentalId, Guid renterUserId) : base(id)
    {
        BookRentalId = bookRentalId;
        RenterUserId = renterUserId;
    }
}