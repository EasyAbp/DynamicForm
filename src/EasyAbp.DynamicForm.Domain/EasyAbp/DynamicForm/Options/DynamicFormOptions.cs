using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Options;

public class DynamicFormOptions
{
    protected Dictionary<string, FormItemTypeDefinition> FormItemTypeDefinitions { get; } = new();
    protected Dictionary<string, FormDefinition> FormDefinitions { get; } = new();

    public FormItemTypeDefinition GetFormItemTypeDefinition([NotNull] string formItemTypeDefinitionName)
    {
        return FormItemTypeDefinitions[formItemTypeDefinitionName];
    }

    public List<FormItemTypeDefinition> GetFormItemTypeDefinitions()
    {
        return FormItemTypeDefinitions.Values.ToList();
    }

    public void AddOrUpdateFormItemTypeDefinition([NotNull] FormItemTypeDefinition formItemTypeDefinition)
    {
        Check.NotNull(formItemTypeDefinition, nameof(formItemTypeDefinition));

        FormItemTypeDefinitions[formItemTypeDefinition.Name] = formItemTypeDefinition;
    }

    public void RemoveFormItemTypeDefinition([NotNull] string formItemTypeDefinitionName)
    {
        FormItemTypeDefinitions.Remove(formItemTypeDefinitionName);
    }

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