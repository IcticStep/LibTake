using Code.Runtime.Logic;

namespace Code.Runtime.Services.Books.Reward
{
    public interface IBookRewardService
    {
        int GetRewardBy(IProgress progress);
    }
}