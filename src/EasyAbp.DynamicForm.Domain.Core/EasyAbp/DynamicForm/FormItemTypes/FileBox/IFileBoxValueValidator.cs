using System.Threading.Tasks;
using EasyAbp.DynamicForm.Shared;

namespace EasyAbp.DynamicForm.FormItemTypes.FileBox;

public interface IFileBoxValueValidator
{
    Task ValidateAsync(IFormItemMetadata metadata, string value);
}