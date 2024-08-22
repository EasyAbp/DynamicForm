namespace EasyAbp.DynamicForm.FormItemTypes.NumberBox;

public enum NumberUi
{
    Default = 0,

    /// <summary>
    /// Normal numerical input box.
    /// </summary>
    NumberInput = 1,

    /// <summary>
    /// The <see cref="Slider"/> provides a ranged number selection.
    /// High-precision decimals or a wider range of numbers may not be well-supported.
    /// </summary>
    Slider = 2,

    /// <summary>
    /// The <see cref="Rating"/> provides a simple stars selection, usually integers like 1 to 5.
    /// High-precision decimals or a wider range of numbers may not be well-supported.
    /// </summary>
    Rating = 3
}