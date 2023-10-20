using System.Collections.Generic;
using System.Linq;
using Code.Runtime.StaticData;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.StaticData
{
    internal sealed class StaticDataService : IStaticDataService
    {
        private Dictionary<string, StaticBook> _books = new();

        public void LoadAll() =>
            LoadBookTypes();

        public void LoadBookTypes() =>
            _books = Resources
                .LoadAll<StaticBook>("Static Data/Books/Instances")
                .ToDictionary(book => book.Id, book => book);

        public StaticBook GetBookData(string id) =>
            _books.TryGetValue(id, out StaticBook result)
                ? result
                : null;
    }
}