using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using JetBrains.Annotations;
using Zenject;

namespace Code.Runtime.Services.Player
{
    [UsedImplicitly]
    internal sealed class PlayerInventoryService : IPlayerInventoryService
    {
        private IPlayerProgressService _progressService;

        public string CurrentBookId => HasBook ? Inventory.Peek() : null;
        public int Count => Inventory.Count;
        public bool HasBook => Count > 0;
        public IReadOnlyList<string> Books => Inventory.AllBooks;

        private PlayerInventoryData Inventory => _progressService.Progress.PlayerData.PlayerInventory;

        public event Action Updated;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        public void InsertBooks(IEnumerable<string> bookIds)
        {
            foreach(string bookId in bookIds)
                InsertBook(bookId);
        }

        public void InsertBook(string id)
        {
            Inventory.Push(id);
            Updated?.Invoke();
        }

        public string RemoveBook()
        {
            string removedId = Inventory.Pop();
            Updated?.Invoke();
            return removedId;
        }

        public void LoadProgress(PlayerProgress progress) =>
            Updated?.Invoke();
    }
}