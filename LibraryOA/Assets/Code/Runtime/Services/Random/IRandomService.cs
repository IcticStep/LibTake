using Code.Runtime.Data;

namespace Code.Runtime.Services.Random
{
    internal interface IRandomService
    {
        int GetInRange(int min, int max);
        float GetInRange(Range range);
    }
}