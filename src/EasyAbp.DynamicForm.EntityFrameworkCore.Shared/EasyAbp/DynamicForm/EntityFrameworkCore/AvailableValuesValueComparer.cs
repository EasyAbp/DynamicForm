using System;
using System.Linq;
using EasyAbp.DynamicForm.Shared;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public class AvailableValuesValueComparer : ValueComparer<AvailableValues>
{
    public AvailableValuesValueComparer()
        : base(
            (d1, d2) => d1.SequenceEqual(d2),
            d => d.Aggregate(0, (k, v) => HashCode.Combine(k, v.GetHashCode())),
            d => new AvailableValues(d))
    {
    }
}