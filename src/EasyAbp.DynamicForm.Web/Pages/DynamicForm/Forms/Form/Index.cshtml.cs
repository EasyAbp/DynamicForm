using System;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form;

public class IndexModel : DynamicFormPageModel
{
    public FormFilterInput FormFilter { get; set; } = new();

    public virtual async Task OnGetAsync(Guid formTemplateId)
    {
        FormFilter.FormTemplateId = formTemplateId;

        await Task.CompletedTask;
    }
}

public class FormFilterInput
{
    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormDefinitionName")]
    public string FormDefinitionName { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormTemplateId")]
    public Guid? FormTemplateId { get; set; }

    [FormControlSize(AbpFormControlSize.Small)]
    [Display(Name = "FormFormTemplateName")]
    public string FormTemplateName { get; set; }
}