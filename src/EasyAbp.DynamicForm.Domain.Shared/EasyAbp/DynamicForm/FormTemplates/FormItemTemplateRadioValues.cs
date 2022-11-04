using System.Collections.Generic;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormItemTemplateRadioValues : List<string>
{
    public FormItemTemplateRadioValues() : base(new List<string>())
    {
    }

    public FormItemTemplateRadioValues(IEnumerable<string> collection) : base(collection)
    {
    }
}