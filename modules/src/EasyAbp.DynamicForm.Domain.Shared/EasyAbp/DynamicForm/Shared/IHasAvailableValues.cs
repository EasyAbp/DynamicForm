namespace EasyAbp.DynamicForm.Shared;

public interface IHasAvailableValues
{
    /// <remarks>
    /// Available radio values. It only affects if not null.
    /// </remarks>
    AvailableValues AvailableValues { get; }
}