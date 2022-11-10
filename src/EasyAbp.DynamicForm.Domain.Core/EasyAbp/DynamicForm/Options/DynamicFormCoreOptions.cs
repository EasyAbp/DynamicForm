using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Options;

public class DynamicFormCoreOptions
{
    protected Dictionary<string, FormItemTypeDefinition> FormItemTypeDefinitions { get; } = new();

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
}