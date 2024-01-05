using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Services.Skills;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.ReadBook
{
    [UsedImplicitly]
    internal sealed class ReadBookService : IReadBookService
    {
        private readonly ISkillService _skillService;
        
        private HashSet<string> _booksRead = new();
        private List<string> _booksReadCacheForSave;
        
        public bool ReadingAllowed { get; private set; } = true;

        public event Action BookRead;

        public ReadBookService(ISkillService skillService)
        {
            _skillService = skillService;
        }

        public void AllowReading() =>
            ReadingAllowed = true;

        public void BlockReading() =>
            ReadingAllowed = false;

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

        public void LoadProgress(Progress progress) =>
            _booksRead = new HashSet<string>(progress.PlayerData.BooksRead);

        public void UpdateProgress(Progress progress)
        {
            UpdateBooksReadCacheForSave();
            progress.PlayerData.BooksRead = _booksReadCacheForSave;
        }

        private void MarkAsRead(string bookId)
        {
            _booksRead.Add(bookId);
            BookRead?.Invoke();
        }

        private void UpdateBooksReadCacheForSave()
        {
            _booksReadCacheForSave ??= new List<string>(_booksRead.Count);
            _booksReadCacheForSave.Clear();
            _booksReadCacheForSave.AddRange(_booksRead);
        }
    }
}