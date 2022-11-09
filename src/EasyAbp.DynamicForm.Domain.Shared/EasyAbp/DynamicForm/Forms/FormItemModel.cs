using EasyAbp.DynamicForm.Shared;
using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Forms;

public class FormItemModel : IFormItem
{
    public string Name { get; set; }

    public string Type { get; set; }

    public string Value { get; set; }

    public FormItemModel([NotNull] string name, [NotNull] string type, [CanBeNull] string inputValue)
    {
        Name = name;
        Type = type;
        Value = inputValue;
    }
}