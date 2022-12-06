using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.Permissions;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Options;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Json;

namespace EasyAbp.DynamicForm.FormTemplates;

public class FormTemplateAppService : CrudAppService<FormTemplate, FormTemplateDto, Guid, FormTemplateGetListInput,
    CreateFormTemplateDto, UpdateFormTemplateDto>, IFormTemplateAppService
{
    protected override string GetPolicyName { get; set; } = DynamicFormPermissions.FormTemplate.Default;
    protected override string GetListPolicyName { get; set; } = DynamicFormPermissions.FormTemplate.Default;
    protected override string CreatePolicyName { get; set; } = DynamicFormPermissions.FormTemplate.Create;
    protected override string UpdatePolicyName { get; set; } = DynamicFormPermissions.FormTemplate.Update;
    protected override string DeletePolicyName { get; set; } = DynamicFormPermissions.FormTemplate.Delete;

    private readonly IJsonSerializer _jsonSerializer;
    private readonly IFormItemProviderResolver _formItemProviderResolver;
    private readonly FormTemplateManager _formTemplateManager;
    private readonly IFormTemplateRepository _repository;

    public FormTemplateAppService(
        IJsonSerializer jsonSerializer,
        IFormItemProviderResolver formItemProviderResolver,
        FormTemplateManager formTemplateManager,
        IFormTemplateRepository repository) : base(repository)
    {
        _jsonSerializer = jsonSerializer;
        _formItemProviderResolver = formItemProviderResolver;
        _formTemplateManager = formTemplateManager;
        _repository = repository;
    }

    protected override async Task<IQueryable<FormTemplate>> CreateFilteredQueryAsync(FormTemplateGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.FormDefinitionName.IsNullOrWhiteSpace(),
                x => x.FormDefinitionName.Contains(input.FormDefinitionName))
            .WhereIf(!input.Name.IsNullOrWhiteSpace(), x => x.Name.Contains(input.Name))
            .WhereIf(!input.CustomTag.IsNullOrWhiteSpace(), x => x.CustomTag.Contains(input.CustomTag))
            ;
    }

    public virtual async Task<DynamicFormBaseInfoDto> GetBaseInfoAsync()
    {
        var options = LazyServiceProvider.LazyGetRequiredService<IOptions<DynamicFormOptions>>();
        var coreOptions = LazyServiceProvider.LazyGetRequiredService<IOptions<DynamicFormCoreOptions>>();

        var formItemTypeDefinitions = coreOptions.Value.GetFormItemTypeDefinitions();
        var configurationJsonTemplates = new Dictionary<string, string>();

        foreach (var definition in formItemTypeDefinitions)
        {
            configurationJsonTemplates[definition.Name] = await GetConfigurationJsonTemplateAsync(definition);
        }

        var formItemTypeDefinitionDtos = formItemTypeDefinitions.Select(x =>
            new FormItemTypeDefinitionDto
            {
                Name = x.Name,
                LocalizationItemKey = x.LocalizationItemKey,
                ConfigurationsJsonTemplate = configurationJsonTemplates[x.Name]
            }).ToList();

        var formDefinitions = options.Value.GetFormDefinitions().Select(x => new FormDefinitionDto
        {
            Name = x.Name,
            DisplayName = x.DisplayName
        }).ToList();

        return new DynamicFormBaseInfoDto
        {
            FormItemTypeDefinitions = formItemTypeDefinitionDtos,
            FormDefinitions = formDefinitions
        };
    }

    protected virtual async Task<string> GetConfigurationJsonTemplateAsync(
        FormItemTypeDefinition formItemTypeDefinition)
    {
        var provider = _formItemProviderResolver.Resolve(formItemTypeDefinition.Name);
        var configurations = await provider.CreateConfigurationsObjectOrNullAsync();

        return configurations is null ? null : _jsonSerializer.Serialize(configurations);
    }

    public override async Task<FormTemplateDto> GetAsync(Guid id)
    {
        var entity = await GetEntityByIdAsync(id);

        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormTemplateOperationInfoModel(entity),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.FormTemplate.Default });
        }

        return await MapToGetOutputDtoAsync(entity);
    }

    public override async Task<PagedResultDto<FormTemplateDto>> GetListAsync(FormTemplateGetListInput input)
    {
        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Manage))
        {
            await AuthorizationService.CheckAsync(new FormTemplateOperationInfoModel
                {
                    FormDefinitionName = input.FormDefinitionName,
                    Name = input.Name,
                    CustomTag = input.CustomTag
                },
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.FormTemplate.Default });
        }

        var query = await CreateFilteredQueryAsync(input);

        var totalCount = await AsyncExecuter.CountAsync(query);

        query = ApplySorting(query, input);
        query = ApplyPaging(query, input);

        var entities = await AsyncExecuter.ToListAsync(query);
        var entityDtos = await MapToGetListOutputDtosAsync(entities);

        return new PagedResultDto<FormTemplateDto>(
            totalCount,
            entityDtos
        );
    }

    [Authorize]
    public override async Task<FormTemplateDto> CreateAsync(CreateFormTemplateDto input)
    {
        var entity = await MapToEntityAsync(input);

        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormTemplateOperationInfoModel(entity),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.FormTemplate.Create });
        }

        await Repository.InsertAsync(entity, autoSave: true);

        return await MapToGetOutputDtoAsync(entity);
    }

    [Authorize]
    public override async Task<FormTemplateDto> UpdateAsync(Guid id, UpdateFormTemplateDto input)
    {
        var entity = await GetEntityByIdAsync(id);

        await CheckUpdatePermissionAsync(entity);

        await MapToEntityAsync(input, entity);
        await Repository.UpdateAsync(entity, autoSave: true);

        return await MapToGetOutputDtoAsync(entity);
    }

    [Authorize]
    public override async Task DeleteAsync(Guid id)
    {
        var entity = await GetEntityByIdAsync(id);

        await AuthorizationService.CheckAsync(CreateFormTemplateOperationInfoModel(entity),
            new OperationAuthorizationRequirement { Name = DynamicFormPermissions.FormTemplate.Delete });

        await _repository.DeleteAsync(entity);
    }

    public virtual async Task<FormTemplateDto> CreateFormItemAsync(Guid id, CreateFormItemTemplateDto input)
    {
        var formTemplate = await GetEntityByIdAsync(id);

        await CheckUpdatePermissionAsync(formTemplate);

        await _formTemplateManager.AddFormItemAsync(formTemplate, input.Name, input.Group, input.InfoText, input.Type,
            input.Optional, MinifyConfigurations(input.Configurations), input.AvailableValues, input.DisplayOrder);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
    }

    public virtual async Task<FormTemplateDto> UpdateFormItemAsync(Guid id, string name,
        UpdateFormItemTemplateDto input)
    {
        var formTemplate = await GetEntityByIdAsync(id);

        await CheckUpdatePermissionAsync(formTemplate);

        await _formTemplateManager.UpdateFormItemAsync(formTemplate, name, input.Group, input.InfoText, input.Type,
            input.Optional, MinifyConfigurations(input.Configurations), input.AvailableValues, input.DisplayOrder);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
    }

    protected virtual string MinifyConfigurations(string inputConfigurations)
    {
        return JToken.Parse(inputConfigurations).ToString(Formatting.None);
    }

    public virtual async Task<FormTemplateDto> DeleteFormItemAsync(Guid id, string name)
    {
        var formTemplate = await GetEntityByIdAsync(id);

        await CheckUpdatePermissionAsync(formTemplate);

        await _formTemplateManager.RemoveFormItemAsync(formTemplate, name);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
    }

    protected virtual async Task CheckUpdatePermissionAsync(FormTemplate formTemplate)
    {
        if (!await AuthorizationService.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Manage))
        {
            await AuthorizationService.CheckAsync(CreateFormTemplateOperationInfoModel(formTemplate),
                new OperationAuthorizationRequirement { Name = DynamicFormPermissions.FormTemplate.Update });
        }
    }

    protected virtual FormTemplateOperationInfoModel CreateFormTemplateOperationInfoModel(FormTemplate formTemplate)
    {
        return new FormTemplateOperationInfoModel
        {
            FormDefinitionName = formTemplate.FormDefinitionName,
            CustomTag = formTemplate.CustomTag,
            FormTemplate = formTemplate
        };
    }

    protected override Task<FormTemplateDto> MapToGetOutputDtoAsync(FormTemplate entity)
    {
        var dto = ObjectMapper.Map<FormTemplate, FormTemplateDto>(entity);

        dto.FormItemTemplates.Sort((x, y) => x.DisplayOrder.CompareTo(y.DisplayOrder));

        return Task.FromResult(dto);
    }

    protected override Task<FormTemplateDto> MapToGetListOutputDtoAsync(FormTemplate entity)
    {
        return MapToGetOutputDtoAsync(entity);
    }

    protected override async Task<FormTemplate> MapToEntityAsync(CreateFormTemplateDto createInput)
    {
        var entity = await _formTemplateManager.CreateAsync(
            createInput.FormDefinitionName, createInput.Name, createInput.CustomTag);

        foreach (var formItemTemplateInput in createInput.FormItemTemplates)
        {
            await _formTemplateManager.AddFormItemAsync(entity, formItemTemplateInput.Name,
                formItemTemplateInput.Group, formItemTemplateInput.InfoText, formItemTemplateInput.Type,
                formItemTemplateInput.Optional, MinifyConfigurations(formItemTemplateInput.Configurations),
                formItemTemplateInput.AvailableValues, formItemTemplateInput.DisplayOrder);
        }

        return entity;
    }

    protected override async Task MapToEntityAsync(UpdateFormTemplateDto updateInput, FormTemplate entity)
    {
        await _formTemplateManager.UpdateAsync(entity, updateInput.Name);
    }
}