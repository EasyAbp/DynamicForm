using System.ComponentModel.DataAnnotations;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;

public class EditFormViewModel
{
    [Display(Name = "FormFormItems")]
    public string FormItems { get; set; }
}
