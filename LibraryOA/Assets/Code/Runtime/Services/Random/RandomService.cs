namespace Code.Runtime.Services.Random
{
    internal sealed class RandomService : IRandomService
    {
        public int GetInRange(int min, int max) =>
            UnityEngine.Random.Range(min, max);
    }
}