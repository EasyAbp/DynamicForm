using System.Collections.Generic;
using System.Linq;

namespace EasyAbp.DynamicForm.Options;

public class DynamicFormOptions
{
    protected Dictionary<string, FormDefinition> FormDefinitions { get; } = new();

    public FormDefinition GetFormDefinition(string formDefinitionName)
    {
        return FormDefinitions[formDefinitionName];
    }

    public List<FormDefinition> GetFormDefinitions()
    {
        return FormDefinitions.Values.ToList();
    }

    public void AddOrUpdateFormDefinition(FormDefinition formDefinition)
    {
        FormDefinitions[formDefinition.Name] = formDefinition;
    }
}