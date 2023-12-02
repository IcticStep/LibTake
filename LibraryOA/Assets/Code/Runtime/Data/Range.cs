using System;

namespace Code.Runtime.Data
{
    [Serializable]
    public readonly struct Range
    {
        public readonly float Min;
        public readonly float Max;

        public Range(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }
}