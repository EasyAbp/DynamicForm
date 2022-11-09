using System;
using EasyAbp.DynamicForm.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public static class DynamicFormEntityTypeBuilderExtensions
{
    public static void TryConfigureAvailableValues(this EntityTypeBuilder b)
    {
        if (b.Metadata.ClrType.IsAssignableTo<IHasAvailableValues>())
        {
            b.Property(nameof(IHasAvailableValues.AvailableValues))
                .HasConversion<AvailableValuesValueConverter>()
                .HasColumnName(nameof(IHasAvailableValues.AvailableValues))
                .Metadata.SetValueComparer(new AvailableValuesValueComparer());
        }
    }
}