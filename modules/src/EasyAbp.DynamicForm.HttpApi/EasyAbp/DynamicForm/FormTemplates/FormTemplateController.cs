using System;
using EasyAbp.DynamicForm.FormTemplates.Dtos;
using Volo.Abp.Application.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;

namespace EasyAbp.DynamicForm.FormTemplates;

[RemoteService(Name = DynamicFormRemoteServiceConsts.RemoteServiceName)]
[Route("/api/dynamic-form/form-template")]
public class FormTemplateController : DynamicFormController, IFormTemplateAppService
{
    private readonly IFormTemplateAppService _service;

    public FormTemplateController(IFormTemplateAppService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<FormTemplateDto> GetAsync(Guid id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    [Route("")]
    public virtual Task<PagedResultDto<FormTemplateDto>> GetListAsync(FormTemplateGetListInput input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPost]
    [Route("")]
    public virtual Task<FormTemplateDto> CreateAsync(CreateFormTemplateDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<FormTemplateDto> UpdateAsync(Guid id, UpdateFormTemplateDto input)
    {
        return _service.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _service.DeleteAsync(id);
    }

    [HttpGet]
    [Route("base-info")]
    public virtual Task<DynamicFormBaseInfoDto> GetBaseInfoAsync()
    {
        return _service.GetBaseInfoAsync();
    }

    [HttpPost]
    [Route("{id}/form-item")]
    public virtual Task<FormTemplateDto> CreateFormItemAsync(Guid id, CreateFormItemTemplateDto input)
    {
        return _service.CreateFormItemAsync(id, input);
    }

    [HttpPut]
    [Route("{id}/form-item/{name}")]
    public virtual Task<FormTemplateDto> UpdateFormItemAsync(Guid id, string name, UpdateFormItemTemplateDto input)
    {
        return _service.UpdateFormItemAsync(id, name, input);
    }

    [HttpDelete]
    [Route("{id}/form-item/{name}")]
    public virtual Task<FormTemplateDto> DeleteFormItemAsync(Guid id, string name)
    {
        return _service.DeleteFormItemAsync(id, name);
    }
}