using System.Collections.Generic;

namespace EasyAbp.DynamicForm.Shared;

public class AvailableRadioValues : List<string>
{
    public AvailableRadioValues() : base(new List<string>())
    {
    }

    public AvailableRadioValues(IEnumerable<string> collection) : base(collection)
    {
    }
}