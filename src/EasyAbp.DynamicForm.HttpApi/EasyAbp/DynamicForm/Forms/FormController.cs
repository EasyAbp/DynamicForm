using System;
using EasyAbp.DynamicForm.Forms.Dtos;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp;
using Volo.Abp.Application.Dtos;

namespace EasyAbp.DynamicForm.Forms;

[RemoteService(Name = DynamicFormRemoteServiceConsts.RemoteServiceName)]
[Route("/api/dynamic-form/form")]
public class FormController : DynamicFormController, IFormAppService
{
    private readonly IFormAppService _service;

    public FormController(IFormAppService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("{id}")]
    public virtual Task<FormDto> GetAsync(Guid id)
    {
        return _service.GetAsync(id);
    }

    [HttpGet]
    [Route("")]
    public virtual Task<PagedResultDto<FormDto>> GetListAsync(FormGetListInput input)
    {
        return _service.GetListAsync(input);
    }

    [HttpPost]
    [Route("")]
    public virtual Task<FormDto> CreateAsync(CreateFormDto input)
    {
        return _service.CreateAsync(input);
    }

    [HttpPut]
    [Route("{id}")]
    public virtual Task<FormDto> UpdateAsync(Guid id, UpdateFormDto input)
    {
        return _service.UpdateAsync(id, input);
    }

    [HttpDelete]
    [Route("{id}")]
    public virtual Task DeleteAsync(Guid id)
    {
        return _service.DeleteAsync(id);
    }
    
    [HttpPut]
    [Route("{id}/form-item/{name}")]
    public virtual Task<FormDto> UpdateFormItemAsync(Guid id, string name, UpdateFormItemDto input)
    {
        return _service.UpdateFormItemAsync(id, name, input);
    }
}