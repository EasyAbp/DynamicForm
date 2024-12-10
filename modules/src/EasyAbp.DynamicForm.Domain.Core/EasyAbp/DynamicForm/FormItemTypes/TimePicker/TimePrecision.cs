namespace EasyAbp.DynamicForm.FormItemTypes.TimePicker;

public enum TimePrecision
{
    Default = 0,

    /// <example>
    /// 1970-01-01T12:55:20.132Z
    /// </example>
    Full = 1,

    /// <example>
    /// 1970-01-01T12:55:20.000Z
    /// </example>
    Seconds = 2,

    /// <example>
    /// 1970-01-01T12:55:00.000Z
    /// </example>
    Minutes = 3,

    /// <example>
    /// 1970-01-01T12:00:00.000Z
    /// </example>
    Hours = 4
}