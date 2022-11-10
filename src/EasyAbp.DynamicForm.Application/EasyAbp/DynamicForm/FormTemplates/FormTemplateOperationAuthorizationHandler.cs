using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyAbp.DynamicForm.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace EasyAbp.DynamicForm.FormTemplates;

public abstract class FormTemplateOperationAuthorizationHandler :
    AuthorizationHandler<OperationAuthorizationRequirement, FormTemplateOperationInfoModel>
{
    protected string[] SpecifiedFormDefinitionNames { get; set; }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource)
    {
        if (!SpecifiedFormDefinitionNames.IsNullOrEmpty() &&
            !SpecifiedFormDefinitionNames.Contains(resource.FormDefinitionName))
        {
            return;
        }

        switch (requirement.Name)
        {
            case DynamicFormPermissions.FormTemplate.Default:
                await HandleGetAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.FormTemplate.Create:
                await HandleCreateAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.FormTemplate.Update:
                await HandleUpdateAsync(context, requirement, resource);
                break;
            case DynamicFormPermissions.FormTemplate.Delete:
                await HandleDeleteAsync(context, requirement, resource);
                break;
        }
    }

    protected abstract Task HandleGetAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource);

    protected abstract Task HandleCreateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource);

    protected abstract Task HandleUpdateAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource);

    protected abstract Task HandleDeleteAsync(AuthorizationHandlerContext context,
        OperationAuthorizationRequirement requirement, FormTemplateOperationInfoModel resource);
}