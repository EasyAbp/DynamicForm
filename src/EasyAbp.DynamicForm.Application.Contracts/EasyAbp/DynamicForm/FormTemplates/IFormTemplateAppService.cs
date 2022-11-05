using System;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using JetBrains.Annotations;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.FormTemplates;

public interface IFormTemplateAppService :
    ICrudAppService<
        FormTemplateDto,
        Guid,
        FormTemplateGetListInput,
        CreateFormTemplateDto,
        UpdateFormTemplateDto>
{
    Task<ListResultDto<FormDefinitionDto>> GetFormDefinitionListAsync();

    Task<FormTemplateDto> CreateFormItemAsync(Guid id, CreateFormItemTemplateDto input);

    Task<FormTemplateDto> UpdateFormItemAsync(Guid id, [NotNull] string name, UpdateFormItemTemplateDto input);

    Task<FormTemplateDto> DeleteFormItemAsync(Guid id, [NotNull] string name);
}