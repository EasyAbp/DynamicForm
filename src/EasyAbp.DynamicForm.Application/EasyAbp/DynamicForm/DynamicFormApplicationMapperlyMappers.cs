using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using Riok.Mapperly.Abstractions;
using Volo.Abp.Mapperly;

namespace EasyAbp.DynamicForm;

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class FormToFormDtoMapper : MapperBase<Form, FormDto>
{
    public override partial FormDto Map(Form source);
    public override partial void Map(Form source, FormDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class FormItemToFormItemDtoMapper : MapperBase<FormItem, FormItemDto>
{
    public override partial FormItemDto Map(FormItem source);
    public override partial void Map(FormItem source, FormItemDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class FormTemplateToFormTemplateDtoMapper : MapperBase<FormTemplate, FormTemplateDto>
{
    public override partial FormTemplateDto Map(FormTemplate source);
    public override partial void Map(FormTemplate source, FormTemplateDto destination);
}

[Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
public partial class FormItemTemplateToFormItemTemplateDtoMapper : MapperBase<FormItemTemplate, FormItemTemplateDto>
{
    public override partial FormItemTemplateDto Map(FormItemTemplate source);
    public override partial void Map(FormItemTemplate source, FormItemTemplateDto destination);
}
