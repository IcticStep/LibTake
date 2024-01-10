using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    [Serializable]
    public class RangeInt
    {
        [SerializeField]
        public int Min;
        [SerializeField]
        public int Max;

        public int Delta => Max - Min;

        public RangeInt(int min, int max)
        {
            Min = min;
            Max = max;
        }
    }
}