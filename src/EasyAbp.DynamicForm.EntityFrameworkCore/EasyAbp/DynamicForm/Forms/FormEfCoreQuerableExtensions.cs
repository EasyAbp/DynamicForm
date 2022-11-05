using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EasyAbp.DynamicForm.Forms;

public static class FormEfCoreQueryableExtensions
{
    public static IQueryable<Form> IncludeDetails(this IQueryable<Form> queryable, bool include = true)
    {
        if (!include)
        {
            return queryable;
        }

        return queryable
                .Include(x => x.FormItems)
            ;
    }
}