using System.Collections.Generic;

namespace EasyAbp.DynamicForm.Shared;

public class AvailableValues : List<string>
{
    public AvailableValues() : base(new List<string>())
    {
    }

    public AvailableValues(IEnumerable<string> collection) : base(collection)
    {
    }
}