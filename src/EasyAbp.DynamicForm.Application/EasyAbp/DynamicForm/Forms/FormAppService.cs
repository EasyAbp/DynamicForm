using System;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using EasyAbp.DynamicForm.Forms.Dtos;
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

    public FormAppService(
        FormManager formManager,
        IFormRepository repository) : base(repository)
    {
        _formManager = formManager;
        _repository = repository;
    }

    protected override async Task<IQueryable<Form>> CreateFilteredQueryAsync(FormGetListInput input)
    {
        return (await base.CreateFilteredQueryAsync(input))
            .WhereIf(!input.FormDefinitionName.IsNullOrWhiteSpace(), x => x.FormDefinitionName.Contains(input.FormDefinitionName))
            .WhereIf(input.FormTemplateId != null, x => x.FormTemplateId == input.FormTemplateId)
            .WhereIf(!input.FormTemplateName.IsNullOrWhiteSpace(), x => x.FormTemplateName.Contains(input.FormTemplateName))
            ;
    }

    protected override async Task<Form> MapToEntityAsync(CreateFormDto createInput)
    {
        throw new NotImplementedException();
    }

    protected override Task MapToEntityAsync(UpdateFormDto updateInput, Form entity)
    {
        throw new NotImplementedException();
    }
}
