using System.Threading.Tasks;
using EasyAbp.DynamicForm.FormItemTypes;
using EasyAbp.DynamicForm.FormItemTypes.ColorPicker;
using EasyAbp.DynamicForm.FormItemTypes.FileBox;
using EasyAbp.DynamicForm.FormItemTypes.NumberBox;
using EasyAbp.DynamicForm.FormItemTypes.OptionButtons;
using EasyAbp.DynamicForm.FormItemTypes.TextBox;
using EasyAbp.DynamicForm.FormItemTypes.TimePicker;
using EasyAbp.DynamicForm.FormItemTypes.Toggle;
using EasyAbp.DynamicForm.FormTemplates;
using EasyAbp.DynamicForm.Shared;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Json;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Uow;

namespace EasyAbp.DynamicForm;

public class DynamicFormDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly IJsonSerializer _jsonSerializer;
    private readonly IFormItemProviderResolver _formItemProviderResolver;
    private readonly IFormTemplateRepository _formTemplateRepository;
    private readonly FormTemplateManager _formTemplateManager;

    public DynamicFormDataSeedContributor(
        ICurrentTenant currentTenant,
        IJsonSerializer jsonSerializer,
        IFormItemProviderResolver formItemProviderResolver,
        IFormTemplateRepository formTemplateRepository,
        FormTemplateManager formTemplateManager)
    {
        _currentTenant = currentTenant;
        _jsonSerializer = jsonSerializer;
        _formItemProviderResolver = formItemProviderResolver;
        _formTemplateRepository = formTemplateRepository;
        _formTemplateManager = formTemplateManager;
    }

    [UnitOfWork(true)]
    public virtual async Task SeedAsync(DataSeedContext context)
    {
        using var change = _currentTenant.Change(context.TenantId);

        await SeedFormTemplatesAsync();
    }

    private async Task SeedFormTemplatesAsync()
    {
        var textBoxFormItemProvider = _formItemProviderResolver.Resolve(TextBoxFormItemProvider.Name);
        var optionButtonsFormItemProvider = _formItemProviderResolver.Resolve(OptionButtonsFormItemProvider.Name);
        var fileBoxFormItemProvider = _formItemProviderResolver.Resolve(FileBoxFormItemProvider.Name);
        var numberBoxFormItemProvider = _formItemProviderResolver.Resolve(NumberBoxFormItemProvider.Name);
        var toggleFormItemProvider = _formItemProviderResolver.Resolve(ToggleFormItemProvider.Name);
        var timePickerFormItemProvider = _formItemProviderResolver.Resolve(TimePickerFormItemProvider.Name);
        var colorPickerFormItemProvider = _formItemProviderResolver.Resolve(ColorPickerFormItemProvider.Name);

        var formTemplate1 = await _formTemplateManager.CreateAsync(DynamicFormTestConsts.TestFormDefinitionName,
            DynamicFormTestConsts.FormTemplate1Name, "my-custom-tag");

        formTemplate1.GetType().GetProperty("Id")!.SetValue(formTemplate1, DynamicFormTestConsts.FormTemplate1Id);

        var formItemTemplate1Configurations =
            (TextBoxFormItemConfigurations)await textBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        formItemTemplate1Configurations!.MinLength = 2;
        formItemTemplate1Configurations!.MaxLength = 10;
        formItemTemplate1Configurations!.RegexPattern = "^[A-Za-z\\s]*$"; // letters and spaces only

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Name", "group1", "Your full name.", TextBoxFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate1Configurations), null, 0, false);

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "DisabledTextBox", "group1", "Disabled text box.", FileBoxFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate1Configurations), null, 0, true);

        var formItemTemplate2Configurations =
            (TextBoxFormItemConfigurations)await textBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Dept", "group1", "Your department name.", TextBoxFormItemProvider.Name, true,
            _jsonSerializer.Serialize(formItemTemplate2Configurations!), ["Dept 1", "Dept 2"], 1,
            false);

        var formItemTemplate3Configurations =
            (OptionButtonsFormItemConfigurations)await optionButtonsFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Gender", "group1", "Your gender.", OptionButtonsFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate3Configurations!), ["Male", "Female"], 2,
            false);

        var formItemTemplate4Configurations =
            (OptionButtonsFormItemConfigurations)await optionButtonsFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        formItemTemplate4Configurations!.IsMultiSelection = true;
        formItemTemplate4Configurations!.MinSelection = 1;
        formItemTemplate4Configurations!.MaxSelection = 2;

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Requirements", "group1", "Other requirements.", OptionButtonsFormItemProvider.Name, true,
            _jsonSerializer.Serialize(formItemTemplate4Configurations),
            ["Use annual leave", "Urgent", "Remote standby"], 3, false);


        var formItemTemplate5Configurations =
            (FileBoxFormItemConfigurations)await fileBoxFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        formItemTemplate5Configurations!.ProviderName = "EasyAbpFileManagement"; // module name
        formItemTemplate5Configurations!.ProviderKey = "UserUpload"; // container name

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Images", "group1", "Images upload.", FileBoxFormItemProvider.Name, true,
            _jsonSerializer.Serialize(formItemTemplate5Configurations), null, 4, false);

        var formItemTemplate6Configurations =
            (NumberBoxFormItemConfigurations)await numberBoxFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        formItemTemplate6Configurations!.MinValue = 10;
        formItemTemplate6Configurations!.MaxValue = 120;
        formItemTemplate6Configurations!.DecimalPlaces = 2;

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Score", "group1", "Your score.", NumberBoxFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate6Configurations), ["100", "100.1", "100.11"], 0, false);

        var formItemTemplate7Configurations =
            (ToggleFormItemConfigurations)await toggleFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "VIP", "group1", "Are you a VIP?", ToggleFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate7Configurations!), ["false"], 0, false);

        var formItemTemplate8Configurations =
            (TimePickerFormItemConfigurations)await timePickerFormItemProvider.CreateConfigurationsObjectOrNullAsync();

        formItemTemplate8Configurations!.TimeDimension = TimeDimension.Day;

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Birthday", "group1", "Your birthday", TimePickerFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate8Configurations!), ["2024-08-21"], 0, false);

        await _formTemplateRepository.InsertAsync(formTemplate1, true);

        var formItemTemplate9Configurations =
            (ColorPickerFormItemConfigurations)await colorPickerFormItemProvider
                .CreateConfigurationsObjectOrNullAsync();

        formItemTemplate9Configurations!.RegexPattern = "^[^0-9]*$"; // should not contain 0-9

        await _formTemplateManager.AddFormItemAsync(
            formTemplate1, "Color", "group1", "Your favorite color", ColorPickerFormItemProvider.Name, false,
            _jsonSerializer.Serialize(formItemTemplate9Configurations!), ["#FFAABBCC", "#000"], 0, false);
    }
}