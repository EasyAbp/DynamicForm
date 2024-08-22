namespace EasyAbp.DynamicForm.FormItemTypes.TimePicker;

public enum TimeDimension
{
    Default = 0,

    /// <example>
    /// 2024-08-21T12:55:20.132Z
    /// </example>
    DateTime = 1,

    /// <example>
    /// 2024-01-01
    /// </example>
    Year = 2,

    /// <example>
    /// 2024-08-01
    /// </example>
    Month = 3,

    /// <example>
    /// 2024-08-21
    /// </example>
    Day = 4,

    /// <example>
    /// 12:55:20.132
    /// </example>
    Time = 5
}