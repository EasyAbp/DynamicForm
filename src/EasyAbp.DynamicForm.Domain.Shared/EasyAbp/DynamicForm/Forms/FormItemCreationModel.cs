using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Forms;

public class FormItemCreationModel : IFormItem
{
    public string Name { get; set; }

    public string Value { get; set; }

    public FormItemCreationModel([NotNull] string name, [CanBeNull] string value)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Value = value;
    }
}