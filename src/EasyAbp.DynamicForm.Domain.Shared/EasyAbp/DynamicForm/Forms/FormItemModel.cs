using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Forms;

public class FormItemModel : IFormItem
{
    public string Name { get; set; }

    public FormItemType Type { get; set; }

    public string Value { get; set; }

    public FormItemModel([NotNull] string name, FormItemType type, [CanBeNull] string inputValue)
    {
        Name = name;
        Type = type;
        Value = inputValue;
    }
}