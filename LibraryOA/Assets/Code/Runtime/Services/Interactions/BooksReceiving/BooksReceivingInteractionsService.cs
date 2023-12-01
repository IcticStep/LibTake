using Code.Runtime.Logic.Customers;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Player;
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

        public bool CanInteract(BookReceiver bookReceiver) =>
            _playerInventoryService.HasBook
            && _playerInventoryService.Count == 1
            && _playerInventoryService.CurrentBookId == bookReceiver.BookId;

        public void Interact(BookReceiver bookReceiver)
        {
            if(!CanInteract(bookReceiver))
                return;

            string bookId = _playerInventoryService.RemoveBook();
            _booksReceivingService.ReceiveBook(bookId);
            bookReceiver.ReceiveBook(bookId);
        }
    }
}