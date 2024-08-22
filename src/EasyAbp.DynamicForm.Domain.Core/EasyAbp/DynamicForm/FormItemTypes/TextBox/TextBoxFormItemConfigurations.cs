using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes.TextBox;

public class TextBoxFormItemConfigurations
{
    public int Rows { get; set; } = 1;

    /// <summary>
    /// This option works when `<see cref="Rows"/> == 1` and `<see cref="TextFormat"/> == <see cref="TextFormat.PlainText"/>`.
    /// </summary>
    public bool IsSecret { get; set; }

    public uint? MaxLength { get; set; }

    public uint? MinLength { get; set; }

    public TextFormat TextFormat { get; set; }

    [CanBeNull]
    public string RegexPattern { get; set; }
}