using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Forms;

public class CreateUpdateFormItemModel : IFormItem
{
    public string Name { get; set; }

    public string Value { get; set; }

    public CreateUpdateFormItemModel([NotNull] string name, [CanBeNull] string value)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        Value = value;
    }
}