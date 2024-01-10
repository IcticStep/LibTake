using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    [Serializable]
    public class RangeFloat
    {
        [SerializeField]
        public float Min;
        [SerializeField]
        public float Max;

        public float Delta => Max - Min;

        public RangeFloat(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}