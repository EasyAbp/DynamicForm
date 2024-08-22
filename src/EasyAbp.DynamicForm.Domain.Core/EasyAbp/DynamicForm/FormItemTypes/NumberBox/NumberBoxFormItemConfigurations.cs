namespace EasyAbp.DynamicForm.FormItemTypes.NumberBox;

public class NumberBoxFormItemConfigurations
{
    public byte DecimalPlaces { get; set; }

    public long? MaxValue { get; set; }

    public long? MinValue { get; set; }

    public NumberUi NumberUi { get; set; }
}