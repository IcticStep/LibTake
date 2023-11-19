using Newtonsoft.Json;

namespace Code.Runtime.Data
{
    internal static class DataExtensions
    {
        public static string ToJson<T>(this T data) =>
            JsonConvert.SerializeObject(data);

        public static T ToDeserialized<T>(this string serialized) =>
            JsonConvert.DeserializeObject<T>(serialized);
    }
}