using System;
using Code.Runtime.Logic;
using Code.Runtime.Services.Player;

namespace Code.Runtime.Services.Interactions
{
    internal sealed class BookSlotInteractService : IBookSlotInteractService
    {
        private readonly IPlayerInventoryService _playerInventoryService;

        public BookSlotInteractService(IPlayerInventoryService playerInventoryService)
        {
            _playerInventoryService = playerInventoryService;
        }

        public bool CanInteract(BookStorage bookStorage) =>
            bookStorage.HasBook || _playerInventoryService.HasBook;

        public void Interact(BookStorage bookStorage)
        {
            if(!CanInteract(bookStorage))
                return;

            if(_playerInventoryService.HasBook && bookStorage.HasBook)
                return;

            if(!_playerInventoryService.HasBook)
            {
                _playerInventoryService.InsertBook();
                bookStorage.RemoveBook();
                return;
            }

            _playerInventoryService.RemoveBook();
            bookStorage.InsertBook();
        }
    }
}