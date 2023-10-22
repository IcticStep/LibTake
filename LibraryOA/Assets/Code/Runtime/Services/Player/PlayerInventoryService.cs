using System;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Player
{
    [UsedImplicitly]
    internal sealed class PlayerInventoryService : IPlayerInventoryService
    {
        private readonly BookStorage _bookStorage = new();

        public string CurrentBookId => _bookStorage.CurrentBookId;
        public bool HasBook => _bookStorage.HasBook;

        public event Action Updated;

        public void InsertBook(string id)
        {
            _bookStorage.InsertBook(id);
            Updated?.Invoke();
        }

        public string RemoveBook()
        {
            string removedId = _bookStorage.RemoveBook();
            Updated?.Invoke();
            return removedId;
        }

        public void LoadProgress(PlayerProgress progress)
        {
            BookData savedData = progress.PlayerInventory;

            if(_bookStorage.HasBook)
                _bookStorage.RemoveBook();
            
            if(savedData is null)
                return;
            
            _bookStorage.InsertBook(savedData.BookId);
        }

        public void UpdateProgress(PlayerProgress progress) =>
            progress.PlayerInventory.BookId = _bookStorage.CurrentBookId;
    }
}