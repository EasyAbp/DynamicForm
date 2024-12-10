using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.AspNetCore.Mvc.UI.Bootstrap.TagHelpers.Form;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;

public class CreateFormViewModel
{
    [HiddenInput]
    [Display(Name = "FormFormTemplateId")]
    public Guid FormTemplateId { get; set; }

    [Display(Name = "FormFormItemTemplates")]
    [TextArea(Rows = 10)]
    [DisabledInput]
    public string FormItemTemplates { get; set; }

    [Display(Name = "FormFormItems")]
    [TextArea(Rows = 10)]
    [Required]
    public string FormItems { get; set; }
}
