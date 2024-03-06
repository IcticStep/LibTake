using System.Collections.Generic;
using System.Linq;
// ReSharper disable PossibleMultipleEnumeration

namespace Code.Runtime.Data
{
    public static class IEnumerableExtensions
    {
        public static T RandomElement<T>(this IEnumerable<T> enumerable) =>
            enumerable.Any()
                ? enumerable.ElementAt(UnityEngine.Random.Range(0, enumerable.Count())) 
                : default(T);
    }
}