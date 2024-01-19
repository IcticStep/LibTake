using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Skills;
using Code.Runtime.StaticData.Books;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    [UsedImplicitly]
    internal sealed class ReadBookService : IReadBookService
    {
        private readonly ISkillService _skillService;
        private readonly IStaticDataService _staticDataService;

        private HashSet<string> _booksRead = new();
        private List<string> _booksReadCacheForSave;
        
        public bool ReadingAllowed { get; private set; } = true;

        public event Action<StaticBook> BookRead;
        public event Action<bool> ReadingPermissionChanged;

        public ReadBookService(ISkillService skillService, IStaticDataService staticDataService)
        {
            _skillService = skillService;
            _staticDataService = staticDataService;
        }

        public void AllowReading()
        {
            ReadingAllowed = true;
            ReadingPermissionChanged?.Invoke(ReadingAllowed);
        }

        public void BlockReading()
        {
            ReadingAllowed = false;
            ReadingPermissionChanged?.Invoke(ReadingAllowed);
        }

        public bool IsRead(string bookId) =>
            _booksRead.Contains(bookId);

        public void ReadBook(string bookId)
        {
            if(!CanReadBook(bookId))
                throw new InvalidOperationException();

            MarkAsRead(bookId);
            _skillService.UpdateSkillsBy(bookId);
        }

        public bool CanReadBook(string bookId) =>
            !IsRead(bookId) && ReadingAllowed;

        public void LoadProgress(GameProgress progress) =>
            _booksRead = new HashSet<string>(progress.PlayerData.BooksRead);

        public void UpdateProgress(GameProgress progress)
        {
            UpdateBooksReadCacheForSave();
            progress.PlayerData.BooksRead = _booksReadCacheForSave;
        }

        private void MarkAsRead(string bookId)
        {
            _booksRead.Add(bookId);
            StaticBook bookRead = _staticDataService.ForBook(bookId);
            BookRead?.Invoke(bookRead);
        }

        private void UpdateBooksReadCacheForSave()
        {
            _booksReadCacheForSave ??= new List<string>(_booksRead.Count);
            _booksReadCacheForSave.Clear();
            _booksReadCacheForSave.AddRange(_booksRead);
        }
    }
}