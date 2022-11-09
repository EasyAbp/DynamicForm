using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using EasyAbp.DynamicForm.Options;
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
    private readonly FormTemplateManager _formTemplateManager;
    private readonly IFormTemplateRepository _repository;

    public FormTemplateAppService(
        IJsonSerializer jsonSerializer,
        FormTemplateManager formTemplateManager,
        IFormTemplateRepository repository) : base(repository)
    {
        _jsonSerializer = jsonSerializer;
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

        var formItemTypeDefinitions = options.Value.GetFormItemTypeDefinitions();
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
        var configurations =
            await ((IFormItemProvider)LazyServiceProvider.LazyGetRequiredService(formItemTypeDefinition.ProviderType))
                .CreateFormItemConfigurationsObjectOrNullAsync();

        return configurations is null ? null : _jsonSerializer.Serialize(configurations);
    }

    public virtual async Task<FormTemplateDto> CreateFormItemAsync(Guid id, CreateFormItemTemplateDto input)
    {
        await CheckUpdatePolicyAsync();

        var formTemplate = await GetEntityByIdAsync(id);

        await _formTemplateManager.CreateFormItemAsync(formTemplate, input.Name, input.InfoText, input.Type,
            input.Optional, MinifyConfigurations(input.Configurations), input.AvailableValues, input.DisplayOrder);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
    }

    public virtual async Task<FormTemplateDto> UpdateFormItemAsync(Guid id, string name,
        UpdateFormItemTemplateDto input)
    {
        await CheckUpdatePolicyAsync();

        var formTemplate = await GetEntityByIdAsync(id);

        await _formTemplateManager.UpdateFormItemAsync(formTemplate, name, input.InfoText, input.Type, input.Optional,
            MinifyConfigurations(input.Configurations), input.AvailableValues, input.DisplayOrder);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
    }

    protected virtual string MinifyConfigurations(string inputConfigurations)
    {
        return JToken.Parse(inputConfigurations).ToString(Formatting.None);
    }

    public virtual async Task<FormTemplateDto> DeleteFormItemAsync(Guid id, string name)
    {
        await CheckUpdatePolicyAsync();

        var formTemplate = await GetEntityByIdAsync(id);

        await _formTemplateManager.RemoveFormItemAsync(formTemplate, name);

        await _repository.UpdateAsync(formTemplate, true);

        return await MapToGetOutputDtoAsync(formTemplate);
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
            await _formTemplateManager.CreateFormItemAsync(entity, formItemTemplateInput.Name,
                formItemTemplateInput.InfoText, formItemTemplateInput.Type, formItemTemplateInput.Optional,
                MinifyConfigurations(formItemTemplateInput.Configurations), formItemTemplateInput.AvailableValues,
                formItemTemplateInput.DisplayOrder);
        }

        return entity;
    }

    protected override async Task MapToEntityAsync(UpdateFormTemplateDto updateInput, FormTemplate entity)
    {
        await _formTemplateManager.UpdateAsync(entity, updateInput.Name);
    }
}