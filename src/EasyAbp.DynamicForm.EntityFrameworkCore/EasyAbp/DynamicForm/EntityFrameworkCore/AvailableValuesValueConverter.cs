using System;
using System.Collections.Generic;
using EasyAbp.DynamicForm.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EasyAbp.DynamicForm.EntityFrameworkCore;

public class AvailableValuesValueConverter : ValueConverter<AvailableValues, string>
{
    public static string Separator { get; set; } = ",";

    public AvailableValuesValueConverter() : base(
        v => v.JoinAsString(Separator),
        v => new AvailableValues(v.Split(Separator, StringSplitOptions.None)))
    {
    }
}