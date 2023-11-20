using System;
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
        public bool HasBook => Inventory.Count > 0;
        private PlayerInventoryData Inventory => _progressService.Progress.PlayerData.PlayerInventory;

        public event Action Updated;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

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
    }
}