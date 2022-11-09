using JetBrains.Annotations;

namespace EasyAbp.DynamicForm.Shared;

public interface IFormItemMetadata
{
    [NotNull]
    string Name { get; }

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

    /// <remarks>
    /// Available radio values. It only affects when the from item type is `Radio`.
    /// </remarks>
    AvailableValues AvailableValues { get; }
}