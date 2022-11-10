using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Options;

public class DynamicFormOptions
{
    protected Dictionary<string, FormDefinition> FormDefinitions { get; } = new();

    public FormDefinition GetFormDefinition([NotNull] string formDefinitionName)
    {
        return FormDefinitions[formDefinitionName];
    }

    public List<FormDefinition> GetFormDefinitions()
    {
        return FormDefinitions.Values.ToList();
    }

    public void AddOrUpdateFormDefinition([NotNull] FormDefinition formDefinition)
    {
        Check.NotNull(formDefinition, nameof(formDefinition));

        FormDefinitions[formDefinition.Name] = formDefinition;
    }

    public void RemoveFormDefinition([NotNull] string formDefinitionName)
    {
        FormDefinitions.Remove(formDefinitionName);
    }
}