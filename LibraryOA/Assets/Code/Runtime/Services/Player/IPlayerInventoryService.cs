using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService : IBookStorage, ISavedProgressReader
    {
        string CurrentBookId { get; }
        int Count { get; }
        bool HasBook { get; }
        IReadOnlyList<string> Books { get; }
        event Action Updated;
        void InsertBooks(IEnumerable<string> bookIds);
        void InsertBook(string id);
        string RemoveBook();
        void LoadProgress(PlayerProgress progress);
    }
}