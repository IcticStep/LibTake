using System;
using System.Collections.Generic;
using Code.Runtime.StaticData;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class SkillStats
    {
        [JsonProperty]
        private Dictionary<BookType, int> _levels = new();

        public event Action Updated;

        public int GetLevelFor(BookType bookType) =>
            _levels.TryGetValue(bookType, out int value)
                ? value
                : default(int);

        public void AddLevelsFor(BookType bookType, int levels)
        {
            _levels.TryAdd(bookType, 0);
            _levels[bookType] += levels;
            Updated?.Invoke();
        }
    }
}