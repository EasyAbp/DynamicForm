using System;
using EasyAbp.DynamicForm.FormItemTypes;
using JetBrains.Annotations;
using Volo.Abp;

namespace EasyAbp.DynamicForm.Options;

public class FormItemTypeDefinition
{
    /// <summary>
    /// Used as Value of `FormItem.Type`.
    /// </summary>
    [NotNull]
    public string Name { get; }

    /// <summary>
    /// Localization item key for UI to display the name of this form item type.
    /// </summary>
    [CanBeNull]
    public string LocalizationItemKey { get; }

    /// <summary>
    /// This type should implement <see cref="IFormItemProvider"/>.
    /// </summary>
    public Type ProviderType { get; }

    public FormItemTypeDefinition(
        [NotNull] string name, [CanBeNull] string localizationItemKey, [NotNull] Type providerType)
    {
        Name = Check.NotNullOrWhiteSpace(name, nameof(name));
        LocalizationItemKey = localizationItemKey;
        ProviderType = Check.NotNull(providerType, nameof(providerType));
    }
}