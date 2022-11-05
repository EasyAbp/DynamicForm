using System.Collections.Generic;

namespace EasyAbp.DynamicForm.Options;

public class DynamicFormOptions
{
    protected Dictionary<string, FormDefinition> FormDefinitions { get; } = new();

    public FormDefinition GetFormDefinition(string formDefinitionName)
    {
        return FormDefinitions[formDefinitionName];
    }

    public void AddOrUpdateFormDefinition(FormDefinition formDefinition)
    {
        FormDefinitions[formDefinition.Name] = formDefinition;
    }
}