using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate;

public class IndexModel : DynamicFormPageModel
{
    public FormTemplateFilterInput FormTemplateFilter { get; set; }
    
    public virtual async Task OnGetAsync()
    {
        await Task.CompletedTask;
    }
}

public class FormTemplateFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateFormDefinitionName")]
    public string FormDefinitionName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateName")]
    public string Name { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormTemplateCustomTag")]
    public string CustomTag { get; set; }

}
