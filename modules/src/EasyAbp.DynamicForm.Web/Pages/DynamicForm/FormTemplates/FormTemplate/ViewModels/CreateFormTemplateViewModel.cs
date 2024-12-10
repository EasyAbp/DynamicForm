using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;

public class CreateFormTemplateViewModel
{
    [Display(Name = "FormTemplateFormDefinitionName")]
    [SelectItems("FormDefinitionNames")]
    public string FormDefinitionName { get; set; }

    [Display(Name = "FormTemplateName")]
    [Required]
    public string Name { get; set; }

    [Display(Name = "FormTemplateCustomTag")]
    public string CustomTag { get; set; }
}
