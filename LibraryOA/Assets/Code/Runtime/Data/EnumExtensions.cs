using System;
using System.Collections.Generic;
using System.Linq;

namespace Code.Runtime.Data
{
    internal static class EnumExtensions
    {
        public static List<TEnum> GetEnumList<TEnum>()
            where TEnum : Enum =>
            ((TEnum[])Enum.GetValues(typeof(TEnum))).ToList();
    }
}