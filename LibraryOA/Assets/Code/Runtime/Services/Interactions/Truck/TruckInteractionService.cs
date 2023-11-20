using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using Code.Runtime.Services.Player;

namespace Code.Runtime.Services.Interactions.Truck
{
    internal sealed class TruckInteractionService
    {
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IPlayerProgressService _playerProgressService;

        public TruckInteractionService(IPlayerInventoryService playerInventoryService, IPlayerProgressService playerProgressService)
        {
            _playerInventoryService = playerInventoryService;
            _playerProgressService = playerProgressService;
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