using UnityEngine;

namespace Code.Runtime.Data
{
    internal static class DataExtensions
    {
        public static string ToJson<T>(this T data) =>
            JsonUtility.ToJson(data);

        public static T ToDeserialized<T>(this string serialized) =>
            JsonUtility.FromJson<T>(serialized);
    }
}