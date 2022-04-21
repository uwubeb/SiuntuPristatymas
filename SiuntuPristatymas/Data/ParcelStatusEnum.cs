using System.ComponentModel;

namespace SiuntuPristatymas.Data;

public enum  ParcelStatusEnum 
{
    [Description("Not assigned")]
    NotAssigned = 0,
    [Description("Waiting for pickup")]
    WaitingForPickup = 1,
    [Description("In transit")]
    InTransit = 2,
    [Description("Delivered")]
    Delivered = 3

}

public static class EnumHelper
{
    public static string GetDescription(this Enum value)
    {
        var type = value.GetType();
        var memberInfo = type.GetMember(value.ToString());
        var attributes = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

        return attributes.Length > 0
            ? ((DescriptionAttribute)attributes[0]).Description
            : value.ToString();
    }
}
