using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    public class BookReceiver : Interactable
    {
        private IBooksReceivingInteractionsService _booksReceivingInteractionsService;
        
        public string BookId { get; private set; }

        public void Initialize(string bookId) =>
            BookId = bookId;

        [Inject]
        private void Construct(IBooksReceivingInteractionsService booksReceivingInteractionsService) =>
            _booksReceivingInteractionsService = booksReceivingInteractionsService;

        private void Reset() =>
            BookId = null;

        public override bool CanInteract() =>
            _booksReceivingInteractionsService.CanInteract(this);

        public override void Interact() =>
            _booksReceivingInteractionsService.Interact(this);
    }
}