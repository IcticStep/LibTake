using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Books;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    [UsedImplicitly]
    internal sealed class ReadBookService : IReadBookService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgress;

        public ReadBookService(IStaticDataService staticDataService, IPlayerProgressService playerProgress)
        {
            _staticDataService = staticDataService;
            _playerProgress = playerProgress;
        }

        public void ReadBook(string bookId)
        {
            StaticBook data = _staticDataService.ForBook(bookId);
            
            MarkAsRead(bookId);
            UpdateSkills(data);
        }

        private void MarkAsRead(string bookId) =>
            _playerProgress.Progress.PlayerData.ReadBooks.AddReadBook(bookId);

        private void UpdateSkills(StaticBook data)
        {
            SkillStats skillStats = _playerProgress.Progress.PlayerData.SkillStats;
            skillStats.AddLevelsFor(data.StaticBookType.BookType, 1);
        }
    }
}