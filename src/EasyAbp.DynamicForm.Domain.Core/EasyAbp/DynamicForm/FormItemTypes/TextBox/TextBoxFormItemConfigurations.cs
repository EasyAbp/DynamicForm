using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes.TextBox;

public class TextBoxFormItemConfigurations
{
    public int? MaxLength { get; set; }

    public int? MinLength { get; set; }

    [CanBeNull]
    public string RegexPattern { get; set; }
}