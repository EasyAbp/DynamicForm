using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;

public class CreateFormItemDataModel
{
    public string Name { get; set; }

    public string Value { get; set; }

    public string Type { get; set; }

    public bool Optional { get; set; }

    public string Configurations { get; set; }

    public AvailableValues AvailableValues { get; set; }
}