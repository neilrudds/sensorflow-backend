namespace SensorFlow.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        public static T ToEnum<T>(this string value) where T : struct
        {
            if (!Enum.TryParse<T>(value, out var enumeration))
            {
                return default;
            }
            return enumeration;
        }
    }
}
