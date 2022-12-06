using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm;

public interface IDynamicFormValidator
{
    Task ValidateTemplatesAsync(IEnumerable<IFormItemMetadata> metadataList);

    Task ValidateValuesAsync(IEnumerable<IFormItemMetadata> metadataList, IEnumerable<IFormItem> formItems);
}