using System.ComponentModel;

namespace RestaurantManagement.Common.Extensions;

public static class EnumExtensions
{
    public static string GetDescription(this Enum e)
    {
        if (e is null) return null;
        var fieldInfo = e.GetType().GetField(e.ToString());

        var attributes = fieldInfo.GetCustomAttributes(typeof(Attribute), false);
        foreach (var attribute in attributes)
        {
            if (attribute is DescriptionAttribute)
                return ((DescriptionAttribute)attribute).Description;
        }
        return e.ToString();
    }
}
