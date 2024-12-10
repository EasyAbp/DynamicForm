using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EasyAbp.DynamicForm.Forms;

public abstract class FormOperationAuthorizationHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, FormOperationInfoModel>
{
    protected string[] SpecifiedFormDefinitionNames { get; set; }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource)
    {
        if (!SpecifiedFormDefinitionNames.IsNullOrEmpty() &&
            !SpecifiedFormDefinitionNames.Contains(resource.FormDefinitionName))
        {
            return;
        }

        switch (requirement.Name)
        {
            case DynamicFormPermissions.Form.Default:
                await HandleGetAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.Form.Create:
                await HandleCreateAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.Form.Update:
                await HandleUpdateAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.Form.Delete:
                await HandleDeleteAsync(context, requirement, resource);
                break;
        }
    }

    protected abstract Task HandleGetAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource);

    protected abstract Task HandleCreateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource);

    protected abstract Task HandleUpdateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource);

    protected abstract Task HandleDeleteAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormOperationInfoModel resource);
}