using System.ComponentModel;

namespace SiuntuPristatymas.Data;

public enum  ParcelStatusEnum 
{
    [Description("Not assigned")]
    NotAssigned,
    [Description("Waiting for pickup")]
    WaitingForPickup,
    [Description("In transit")]
    InTransit,
    [Description("Delivered")]
    Delivered,

}

public static class EnumHelper
{
    private static TEnum? GetEnum<TEnum>(string value) where TEnum : struct
    {
        TEnum result;

        return Enum.TryParse<TEnum>(value, out result) ? (TEnum?)result : null;
    }

}
