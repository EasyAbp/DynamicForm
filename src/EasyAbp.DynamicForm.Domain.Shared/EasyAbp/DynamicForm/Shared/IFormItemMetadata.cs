using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Shared;

public interface IFormItemMetadata : IHasAvailableValues
{
    [NotNull]
    string Name { get; }

    /// <summary>
    /// Front-end can group the form items with this group name.
    /// </summary>
    [CanBeNull]
    string Group { get; }

    /// <summary>
    /// Info text (tips) for the form item.
    /// </summary>
    [CanBeNull]
    string InfoText { get; }

    /// <summary>
    /// Type name of the form item. Types should register in the DynamicFormOptions.
    /// </summary>
    [NotNull]
    string Type { get; }

    /// <remarks>
    /// If it's set to <c>true</c>, a string form item can't be <c>null</c>, empty, or white space.
    /// </remarks>
    bool Optional { get; }

    /// <summary>
    /// JSON serialized form item configurations.
    /// </summary>
    [CanBeNull]
    string Configurations { get; }

    /// <summary>
    /// Display order of the form item collection.
    /// </summary>
    int DisplayOrder { get; }

    /// <summary>
    /// Disabled form items should not be validated and not show in UI.
    /// </summary>
    bool Disabled { get; }
}