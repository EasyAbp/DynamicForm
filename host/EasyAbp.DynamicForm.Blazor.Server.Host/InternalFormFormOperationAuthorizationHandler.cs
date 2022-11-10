using System.Threading.Tasks;
using EasyAbp.DynamicForm.Forms;
using EasyAbp.DynamicForm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;

namespace EasyAbp.DynamicForm.Blazor.Server.Host;

public class InternalFormFormOperationAuthorizationHandler : FormOperationAuthorizationHandler, ISingletonDependency
{
    private readonly IPermissionChecker _permissionChecker;

    public InternalFormFormOperationAuthorizationHandler(
        IPermissionChecker permissionChecker)
    {
        _permissionChecker = permissionChecker;
        SpecifiedFormDefinitionNames = new[] { "InternalForm" };
    }

    protected override async Task HandleGetAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource)
    {
        if (await _permissionChecker.IsGrantedAsync(DynamicFormPermissions.Form.Default))
        {
            context.Succeed(requirement);
        }
    }

    protected override async Task HandleCreateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource)
    {
        if (await _permissionChecker.IsGrantedAsync(DynamicFormPermissions.Form.Create))
        {
            context.Succeed(requirement);
        }
    }

    protected override Task HandleUpdateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource)
    {
        context.Fail();
        return Task.CompletedTask;
    }

    protected override Task HandleDeleteAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource)
    {
        context.Fail();
        return Task.CompletedTask;
    }
}