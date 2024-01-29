using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.StaticData.Books;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Skills
{
    [UsedImplicitly]
    internal sealed class SkillService : ISkillService
    {
        private readonly IStaticDataService _staticDataService;
        
        private Dictionary<BookType, int> _levels = new();

        public event Action Updated;

        public SkillService(IStaticDataService staticDataService)
        {
            _staticDataService = staticDataService;
        }

        public void UpdateSkillsBy(string bookId)
        {
            StaticBook bookData = _staticDataService.ForBook(bookId);
            
            AddLevelsFor(bookData.StaticBookType.BookType, 1);
            Updated?.Invoke();
        }

        public int GetSkillByBookType(BookType bookType) =>
            _levels.GetValueOrDefault(bookType);

        public void LoadProgress(GameProgress progress) =>
            _levels = progress.PlayerData.Skills;

        public void UpdateProgress(GameProgress progress) =>
            progress.PlayerData.Skills = _levels;

        public void CleanUp() =>
            _levels.Clear();

        private void AddLevelsFor(BookType bookType, int levels)
        {
            _levels.TryAdd(bookType, 0);
            _levels[bookType] += levels;
            Updated?.Invoke();
        }
    }
}