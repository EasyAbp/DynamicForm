using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using EasyAbp.DynamicForm.Forms.Dtos;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Localization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Application.Dtos;
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

        LocalizationResource = typeof(DynamicFormResource);
        ObjectMapperContext = typeof(DynamicFormApplicationModule);
    }

    protected override async Task<IQueryable<Form>> CreateFilteredQueryAsync(FormGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.FormDefinitionName.IsNullOrWhiteSpace(),
                x => x.FormDefinitionName.Contains(input.FormDefinitionName))
            .WhereIf(input.FormTemplateId != null, x => x.FormTemplateId == input.FormTemplateId)
            .WhereIf(!input.FormTemplateName.IsNullOrWhiteSpace(),
                x => x.FormTemplateName.Contains(input.FormTemplateName))
            .WhereIf(input.CreatorId != null, x => x.CreatorId == input.CreatorId)
            ;
    }

    protected override async Task<Form> MapToEntityAsync(CreateFormDto createInput)
    {
        var formTemplate = await _formTemplateRepository.GetAsync(createInput.FormTemplateId);

        var entity = await _formManager.CreateAsync(formTemplate,
            createInput.FormItems.Select(x => new CreateUpdateFormItemModel(x.Name, x.Value)));

        return entity;
    }

    public override async Task<FormDto> GetAsync(Guid id)
    {
        var entity = await GetEntityByIdAsync(id);

        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.Form.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormOperationInfoModel(entity),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.Form.Default });
        }

        return await MapToGetOutputDtoAsync(entity);
    }

    public override async Task<PagedResultDto<FormDto>> GetListAsync(FormGetListInput input)
    {
        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.Form.Manage))
        {
            await AuthorizationService.CheckAsync(new FormOperationInfoModel
                {
                    FormDefinitionName = input.FormDefinitionName,
                    FormTemplateId = input.FormTemplateId,
                    CreatorId = input.CreatorId
                },
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.Form.Default });
        }

        var query = await CreateFilteredQueryAsync(input);

        var totalCount = await AsyncExecuter.CountAsync(query);

        query = ApplySorting(query, input);
        query = ApplyPaging(query, input);

        var entities = await AsyncExecuter.ToListAsync(query);
        var entityDtos = await MapToGetListOutputDtosAsync(entities);

        return new PagedResultDto<FormDto>(
            totalCount,
            entityDtos
        );
    }

    public override async Task<FormDto> CreateAsync(CreateFormDto input)
    {
        var entity = await MapToEntityAsync(input);

        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.Form.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormOperationInfoModel(entity),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.Form.Create });
        }

        await Repository.InsertAsync(entity, autoSave: true);

        return await MapToGetOutputDtoAsync(entity);
    }

    [Authorize]
    public virtual async Task<FormDto> UpdateFormItemAsync(Guid id, string name, UpdateFormItemDto input)
    {
        var entity = await GetEntityByIdAsync(id);

        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.Form.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormOperationInfoModel(entity),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.Form.Update });
        }

        await _formManager.UpdateFormItemAsync(entity, new CreateUpdateFormItemModel(name, input.Value));

        await _repository.UpdateAsync(entity, true);

        return await MapToGetOutputDtoAsync(entity);
    }

    [Authorize]
    public override async Task DeleteAsync(Guid id)
    {
        var entity = await GetEntityByIdAsync(id);

        await AuthorizationService.CheckAsync(CreateFormOperationInfoModel(entity),
            new OperationAuthorizationRequirement { Name = DynamicFormPermissions.Form.Delete });

        await _repository.DeleteAsync(entity);
    }

    protected virtual FormOperationInfoModel CreateFormOperationInfoModel(Form form)
    {
        return new FormOperationInfoModel
        {
            FormDefinitionName = form.FormDefinitionName,
            FormTemplateId = form.FormTemplateId,
            CreatorId = form.CreatorId,
            Form = form
        };
    }

    protected override Task<FormDto> MapToGetOutputDtoAsync(Form entity)
    {
        var dto = ObjectMapper.Map<Form, FormDto>(entity);

        dto.FormItems.Sort((x, y) => x.DisplayOrder.CompareTo(y.DisplayOrder));

        return Task.FromResult(dto);
    }

    protected override Task<FormDto> MapToGetListOutputDtoAsync(Form entity)
    {
        return MapToGetOutputDtoAsync(entity);
    }
}