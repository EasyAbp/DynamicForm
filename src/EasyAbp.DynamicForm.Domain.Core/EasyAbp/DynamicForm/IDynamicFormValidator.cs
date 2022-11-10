using System.Collections.Generic;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm;

public interface IDynamicFormValidator
{
    Task ValidateTemplatesAsync(IEnumerable<IFormItemTemplate> templates);

    Task ValidateValuesAsync(IEnumerable<IFormItemMetadata> metadataList, IEnumerable<IFormItem> formItems);
}