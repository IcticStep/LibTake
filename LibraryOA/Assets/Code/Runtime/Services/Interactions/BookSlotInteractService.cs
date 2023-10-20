using Code.Runtime.Data;
using Code.Runtime.Services.Player;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions
{
    [UsedImplicitly]
    internal sealed class BookSlotInteractService : IBookSlotInteractService
    {
        private readonly IPlayerInventoryService _playerInventoryService;

        public BookSlotInteractService(IPlayerInventoryService playerInventoryService)
        {
            _playerInventoryService = playerInventoryService;
        }

        public bool CanInteract(IBookStorage bookStorage) =>
            bookStorage.HasBook || _playerInventoryService.HasBook;

        public void Interact(IBookStorage bookStorage)
        {
            if(!CanInteract(bookStorage))
                return;

            if(_playerInventoryService.HasBook && bookStorage.HasBook)
                return;

            string bookId;
            if(!_playerInventoryService.HasBook)
            {
                bookId = bookStorage.RemoveBook();
                _playerInventoryService.InsertBook(bookId);
                return;
            }

            bookId = _playerInventoryService.RemoveBook();
            bookStorage.InsertBook(bookId);
        }
    }
}