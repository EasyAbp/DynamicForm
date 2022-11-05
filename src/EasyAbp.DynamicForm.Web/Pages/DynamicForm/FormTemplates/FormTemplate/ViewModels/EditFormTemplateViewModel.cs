using System.ComponentModel.DataAnnotations;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;

public class EditFormTemplateViewModel
{
    [Display(Name = "FormTemplateName")]
    public string Name { get; set; }
}
