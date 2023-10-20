using System;
using Code.Runtime.Data;
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
    }
}