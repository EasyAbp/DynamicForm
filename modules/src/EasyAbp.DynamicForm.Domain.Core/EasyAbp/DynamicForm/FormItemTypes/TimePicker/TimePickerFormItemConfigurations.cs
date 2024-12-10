namespace EasyAbp.DynamicForm.FormItemTypes.TimePicker;

public class TimePickerFormItemConfigurations
{
    public TimeDimension TimeDimension { get; set; }

    public TimePrecision TimePrecision { get; set; }

    /// <summary>
    /// If true, the value represents a time range (including start and end times).
    /// Example for a single time: 2024-08-21T12:55:20.132Z.
    /// Example for a ranged time: 2024-08-21T12:55:20.132Z;2024-08-22T12:55:20.132Z.
    /// </summary>
    public bool IsTimeRange { get; set; }
}