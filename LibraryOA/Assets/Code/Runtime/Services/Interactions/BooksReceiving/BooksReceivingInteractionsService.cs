using Code.Runtime.Logic.Customers;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Player;
using Code.Runtime.Services.Player.Inventory;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.BooksReceiving
{
    [UsedImplicitly]
    internal sealed class BooksReceivingInteractionsService : IBooksReceivingInteractionsService
    {
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IPlayerInventoryService _playerInventoryService;

        public BooksReceivingInteractionsService(IBooksReceivingService booksReceivingService, IPlayerInventoryService playerInventoryService)
        {
            _booksReceivingService = booksReceivingService;
            _playerInventoryService = playerInventoryService;
        }

        public bool CanInteract(IBookReceiver bookReceiver) =>
            _playerInventoryService.HasBook
            && _playerInventoryService.BooksCount == 1
            && _playerInventoryService.CurrentBookId == bookReceiver.BookId;

        public void Interact(IBookReceiver bookReceiver)
        {
            if(!CanInteract(bookReceiver))
                return;

            string bookId = _playerInventoryService.RemoveBook();
            _booksReceivingService.ReceiveBook(bookId);
            bookReceiver.ReceiveBook(bookId);
        }
    }
}