using System;
using System.Collections.Generic;
using System.Linq;
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
        private readonly IPersistantProgressService _persistantProgress;
        private HashSet<string> _booksRead = new();
        private List<string> _booksReadCacheForSave;
        
        public bool ReadingAllowed { get; private set; } = true;

        public event Action BookRead;
        public event Action SkillGained;

        public ReadBookService(IStaticDataService staticDataService, IPersistantProgressService persistantProgress)
        {
            _staticDataService = staticDataService;
            _persistantProgress = persistantProgress;
        }

        public void AllowReading() =>
            ReadingAllowed = true;

        public void BlockReading() =>
            ReadingAllowed = false;

        public bool IsRead(string bookId) =>
            _booksRead.Contains(bookId);

        public void ReadBook(string bookId)
        {
            StaticBook data = _staticDataService.ForBook(bookId);
            
            MarkAsRead(bookId);
            UpdateSkills(data);
            
            BookRead?.Invoke();
        }

        public bool CanReadBook(string bookId) =>
            !IsRead(bookId) && ReadingAllowed;

        public void LoadProgress(Progress progress) =>
            _booksRead = new HashSet<string>(progress.PlayerData.BooksRead);

        public void UpdateProgress(Progress progress)
        {
            UpdateBooksReadCacheForSave();
            progress.PlayerData.BooksRead = _booksReadCacheForSave;
        }

        private void MarkAsRead(string bookId) =>
            _booksRead.Add(bookId);

        private void UpdateSkills(StaticBook data)
        {
            SkillStats skillStats = _persistantProgress.Progress.PlayerData.SkillStats;
            skillStats.AddLevelsFor(data.StaticBookType.BookType, 1);
            SkillGained?.Invoke();
        }

        private void UpdateBooksReadCacheForSave()
        {
            _booksReadCacheForSave ??= new List<string>(_booksRead.Count);
            _booksReadCacheForSave.Clear();
            _booksReadCacheForSave.AddRange(_booksRead);
        }
    }
}