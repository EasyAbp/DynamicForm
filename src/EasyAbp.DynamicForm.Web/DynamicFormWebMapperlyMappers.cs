using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Web.Pages.DynamicForm.FormTemplates.FormTemplate.ViewModels;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.DynamicForm.Web;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class FormTemplateDtoToEditFormTemplateViewModelMapper : MapperBase<FormTemplateDto, EditFormTemplateViewModel>
{
    public override partial EditFormTemplateViewModel Map(FormTemplateDto source);
    public override partial void Map(FormTemplateDto source, EditFormTemplateViewModel destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class EditFormTemplateViewModelToUpdateFormTemplateDtoMapper : MapperBase<EditFormTemplateViewModel, UpdateFormTemplateDto>
{
    public override partial UpdateFormTemplateDto Map(EditFormTemplateViewModel source);
    public override partial void Map(EditFormTemplateViewModel source, UpdateFormTemplateDto destination);
}
