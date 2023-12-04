using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Books;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    [UsedImplicitly]
    internal sealed class ReadBookService : IReadBookService
    {
        private readonly IStaticDataService _staticDataService;
        private readonly IPlayerProgressService _playerProgress;
        
        public bool ReadingAllowed { get; private set; } = true;

        public ReadBookService(IStaticDataService staticDataService, IPlayerProgressService playerProgress)
        {
            _staticDataService = staticDataService;
            _playerProgress = playerProgress;
        }

        public void AllowReading() =>
            ReadingAllowed = true;

        public void BlockReading() =>
            ReadingAllowed = false;

        public void ReadBook(string bookId)
        {
            StaticBook data = _staticDataService.ForBook(bookId);
            
            MarkAsRead(bookId);
            UpdateSkills(data);
        }

        public bool CanReadBook(string bookId) =>
            !IsRead(bookId) && ReadingAllowed;

        public bool IsRead(string bookId) =>
            _playerProgress.Progress.PlayerData.ReadBooks.IsBookRead(bookId);

        private void MarkAsRead(string bookId) =>
            _playerProgress.Progress.PlayerData.ReadBooks.AddReadBook(bookId);

        private void UpdateSkills(StaticBook data)
        {
            SkillStats skillStats = _playerProgress.Progress.PlayerData.SkillStats;
            skillStats.AddLevelsFor(data.StaticBookType.BookType, 1);
        }
    }
}