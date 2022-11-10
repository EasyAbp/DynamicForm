using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm;

public class InternalFormFormTemplateOperationAuthorizationHandler : FormTemplateOperationAuthorizationHandler,
    ISingletonDependency
{
    private readonly IPermissionChecker _permissionChecker;

    public InternalFormFormTemplateOperationAuthorizationHandler(
        IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
        SpecifiedFormDefinitionNames = new[] { "InternalForm" };
    }

    protected override async Task HandleGetAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource)
    {
        if (await _permissionChecker.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Default))
        {
            context.Succeed(requirement);
        }
    }

    protected override async Task HandleCreateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource)
    {
        if (await _permissionChecker.IsGrantedAsync(DynamicFormPermissions.FormTemplate.Create))
        {
            context.Succeed(requirement);
        }
    }

    protected override Task HandleUpdateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource)
    {
        context.Fail();
        return Task.CompletedTask;
    }

    protected override Task HandleDeleteAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource)
    {
        context.Fail();
        return Task.CompletedTask;
    }
}