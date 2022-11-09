using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Options;

public class FormDefinition
{
    [NotNull]
    public string Name { get; }

    [CanBeNull]
    public string DisplayName { get; }

    /// <summary>
    /// Should implement the ICustomFormItemValidator interface.
    /// </summary>
    public List<Type> CustomFormItemValidatorTypes { get; }

    public FormDefinition(
        [NotNull] string name,
        [CanBeNull] string displayName,
        List<Type> customFormItemValidatorTypes = null)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        DisplayName = displayName;
        CustomFormItemValidatorTypes = customFormItemValidatorTypes ?? new List<Type>();
    }
}