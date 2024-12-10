using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.FormTemplates;

public static class FormTemplateEfCoreQueryableExtensions
{
    public static IQueryable<FormTemplate> IncludeDetails(this IQueryable<FormTemplate> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
                .Include(x => x.FormItemTemplates)
            ;
    }
}