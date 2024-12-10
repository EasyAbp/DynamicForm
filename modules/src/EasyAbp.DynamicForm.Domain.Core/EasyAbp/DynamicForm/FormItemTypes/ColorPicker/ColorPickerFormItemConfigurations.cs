using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.FormItemTypes.ColorPicker;

public class ColorPickerFormItemConfigurations
{
    [CanBeNull]
    public string RegexPattern { get; set; }
}