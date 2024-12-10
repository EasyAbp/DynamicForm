using System.Threading.Tasks;
using Volo.Abp.Application.Services;

namespace EasyAbp.DynamicForm.UnifiedDemo.SettingManagement;

public interface IUnifiedDemoSettingsAppService : IApplicationService
{
    Task<UnifiedDemoSettingsDto> GetAsync();

    Task UpdateAsync(UpdateUnifiedDemoSettingsDto input);
}
