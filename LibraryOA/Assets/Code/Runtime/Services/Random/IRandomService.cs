namespace Code.Runtime.Services.Random
{
    internal interface IRandomService
    {
        int GetInRange(int min, int max);
    }
}