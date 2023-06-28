using System.Collections.Generic;
using EasyAbp.DynamicForm.Forms;

namespace EasyAbp.DynamicForm;

public static class FormItemTestHelper
{
    public static List<CreateUpdateFormItemModel> CreateStandardFormItems()
    {
        return new List<CreateUpdateFormItemModel>
        {
            new("Name", "John"),
            new("DisabledTextBox", ""), // this item is NOT optional, but disabled
            new("Dept", "Dept 2"),
            new("Gender", "Male"),
            new("Requirements", "Use annual leave,Urgent"),
            new("Images", "[\"https://my-fake-site1.com/1.png\", \"https://my-fake-site1.com/2.png\"]"),
        };
    }
}