using System;
using System.ComponentModel.DataAnnotations;

namespace EasyAbp.DynamicForm.Web.Pages.DynamicForm.Forms.Form.ViewModels;

public class CreateFormViewModel
{
    [Display(Name = "FormFormTemplateId")]
    public Guid FormTemplateId { get; set; }

    [Display(Name = "FormFormItems")]
    public string FormItems { get; set; }
}
