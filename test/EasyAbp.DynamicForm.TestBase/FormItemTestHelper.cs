using System.Collections.Generic;
using EasyAbp.DynamicForm.Forms;

namespace EasyAbp.DynamicForm;

public static class FormItemTestHelper
{
    public static List<FormItemCreationModel> CreateStandardFormItems()
    {
        return new List<FormItemCreationModel>
        {
            new("Name", "John"),
            new("Dept", "Dept 2"),
            new("Gender", "Male"),
            new("Requirements", "Use annual leave,Urgent"),
            new("Images", "[\"https://my-fake-site1.com/1.png\", \"https://my-fake-site1.com/2.png\"]"),
        };
    }
}