using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Books.Reward
{
    [UsedImplicitly]
    internal sealed class BookRewardService : IBookRewardService
    {
        private readonly IStaticDataService _staticDataService;
        
        public BookRewardService(IStaticDataService staticDataService) 
        {
            _staticDataService = staticDataService;
        }
        
        public int GetRewardBy(IProgress progress)
        {
            float progressRemained = progress.MaxValue - progress.Value;
            return _staticDataService.BookReceiving.BookRewards.GetRewardSize(progressRemained);
        }
    }
}