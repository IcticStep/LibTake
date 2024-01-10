using System;
using UnityEngine;

namespace Code.Runtime.Data
{
    [Serializable]
    public class Range
    {
        [SerializeField]
        public float Min;
        [SerializeField]
        public float Max;

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}