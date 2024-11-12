using System.ComponentModel;
using System.Reflection;

namespace StargateAPI.Business.Enums
{
    public static class EnumExtensions
    {
        public static string GetPrettyDescription(this Enum value)
        {
            var field = value.GetType().GetField(value.ToString());

            if (field is null)
                return value.ToString();  // Fallback: Return the enum name if no field found

            var attribute = (DescriptionAttribute?)field.GetCustomAttribute(typeof(DescriptionAttribute));

            return attribute is null ? value.ToString() : attribute.Description;
        }

        public static string GetValuesAsString<T>() where T : Enum
        {
            return string.Join(", ", Enum.GetNames(typeof(T)));
        }
    }
}