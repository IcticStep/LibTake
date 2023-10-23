using Code.Runtime.Data;
using Code.Runtime.Services.Player;

namespace Code.Runtime.Services.Interactions
{
    internal sealed class ReadingTableInteractionService : IReadingTableInteractionService
    {
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IBookSlotInteractionService _bookSlotInteractionService;

        public ReadingTableInteractionService(IPlayerInventoryService playerInventoryService, IBookSlotInteractionService bookSlotInteractionService)
        {
            _playerInventoryService = playerInventoryService;
            _bookSlotInteractionService = bookSlotInteractionService;
        }

        public bool CanInteract(IBookStorage bookStorage) =>
            _bookSlotInteractionService.CanInteract(bookStorage);

        public void Interact(IBookStorage bookStorage) =>
            _bookSlotInteractionService.Interact(bookStorage);
    }
}