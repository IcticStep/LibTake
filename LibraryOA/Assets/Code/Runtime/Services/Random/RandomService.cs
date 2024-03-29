using Code.Runtime.Data;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Random
{
    [UsedImplicitly]
    internal sealed class RandomService : IRandomService
    {
        public int GetInRange(int min, int max) =>
            UnityEngine.Random.Range(min, max);
        
        public float GetInRange(RangeFloat rangeFloat) =>
            UnityEngine.Random.Range(rangeFloat.Min, rangeFloat.Max);
    }
}