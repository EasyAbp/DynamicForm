using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.Forms;

public class FormAppService : CrudAppService<Form, FormDto, Guid, FormGetListInput, CreateFormDto, UpdateFormDto>,
    IFormAppService
{
    protected override string GetPolicyName { get; set; } = DynamicFormPermissions.Form.Default;
    protected override string GetListPolicyName { get; set; } = DynamicFormPermissions.Form.Default;
    protected override string CreatePolicyName { get; set; } = DynamicFormPermissions.Form.Create;
    protected override string UpdatePolicyName { get; set; } = DynamicFormPermissions.Form.Update;
    protected override string DeletePolicyName { get; set; } = DynamicFormPermissions.Form.Delete;

    private readonly FormManager _formManager;
    private readonly IFormRepository _repository;
    private readonly IFormTemplateRepository _formTemplateRepository;

    public FormAppService(
        FormManager formManager,
        IFormRepository repository,
        IFormTemplateRepository formTemplateRepository) : base(repository)
    {
        _formManager = formManager;
        _repository = repository;
        _formTemplateRepository = formTemplateRepository;
    }

    protected override async Task<IQueryable<Form>> CreateFilteredQueryAsync(FormGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.FormDefinitionName.IsNullOrWhiteSpace(),
                x => x.FormDefinitionName.Contains(input.FormDefinitionName))
            .WhereIf(input.FormTemplateId != null, x => x.FormTemplateId == input.FormTemplateId)
            .WhereIf(!input.FormTemplateName.IsNullOrWhiteSpace(),
                x => x.FormTemplateName.Contains(input.FormTemplateName))
            ;
    }

    protected override async Task<Form> MapToEntityAsync(CreateFormDto createInput)
    {
        var formTemplate = await _formTemplateRepository.GetAsync(createInput.FormTemplateId);

        var entity = await _formManager.CreateAsync(formTemplate);

        foreach (var inputFormItem in createInput.FormItems)
        {
            var metadata = formTemplate.FindFormItemTemplate(inputFormItem.Name);

            await _formManager.CreateFormItemAsync(entity, inputFormItem.Name, metadata, inputFormItem.Value);
        }

        return entity;
    }

    public virtual async Task<FormDto> UpdateFormItemAsync(Guid id, string name, UpdateFormItemDto input)
    {
        // Todo: authorization handler
        await CheckUpdatePolicyAsync();

        var entity = await GetEntityByIdAsync(id);

        await _formManager.UpdateFormItemAsync(entity, name, input.Value);

        await _repository.UpdateAsync(entity, true);

        return await MapToGetOutputDtoAsync(entity);
    }
}