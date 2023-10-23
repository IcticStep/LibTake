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
        private readonly BookStorage _bookStorage = new();
        private IPlayerProgressService _progressService;

        public string CurrentBookId => _bookStorage.CurrentBookId;
        public bool HasBook => _bookStorage.HasBook;

        public event Action Updated;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        public void InsertBook(string id)
        {
            _bookStorage.InsertBook(id);
            UpdateProgress(_progressService.Progress);
            Updated?.Invoke();
        }

        public void LoadProgress(PlayerProgress progress)
        {
            BookData savedData = progress.PlayerData.BookInHands;

            if(_bookStorage.HasBook)
                _bookStorage.RemoveBook();
            
            if(savedData is null)
                return;
            
            _bookStorage.InsertBook(savedData.BookId);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.PlayerData.BookInHands.BookId = _bookStorage.CurrentBookId;

        public string RemoveBook()
        {
            string removedId = _bookStorage.RemoveBook();
            UpdateProgress(_progressService.Progress);
            Updated?.Invoke();
            return removedId;
        }
    }
}