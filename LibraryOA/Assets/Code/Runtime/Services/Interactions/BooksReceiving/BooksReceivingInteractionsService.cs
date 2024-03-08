using System;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.Services.Player.Inventory;
using JetBrains.Annotations;

namespace Code.Runtime.Services.Interactions.BooksReceiving
{
    [UsedImplicitly]
    internal sealed class BooksReceivingInteractionsService : IBooksReceivingInteractionsService
    {
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IPlayerInventoryService _playerInventoryService;
        
        public event Action BooksReceived;

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
            BooksReceived?.Invoke();
        }
    }
}