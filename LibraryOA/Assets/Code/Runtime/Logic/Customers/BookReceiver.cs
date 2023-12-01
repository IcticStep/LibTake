using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    public class BookReceiver : Interactable
    {
        [SerializeField]
        private BookStorageHolder _bookStorageHolder;

        private UniTaskCompletionSource _taskCompletionSource;
        private IBooksReceivingInteractionsService _booksReceivingInteractionsService;
        
        public string BookId { get; private set; }
        public UniTask ReceivingTask => _taskCompletionSource.Task;

        public void Initialize(string bookIdRequested)
        {
            _taskCompletionSource = new UniTaskCompletionSource();
            BookId = bookIdRequested;
        }

        public void ReceiveBook(string bookId)
        {
            _bookStorageHolder.BookStorage.InsertBook(bookId);
            _taskCompletionSource.TrySetResult();
        }

        [Inject]
        private void Construct(IBooksReceivingInteractionsService booksReceivingInteractionsService) =>
            _booksReceivingInteractionsService = booksReceivingInteractionsService;

        public void Reset()
        {
            BookId = null;
            _taskCompletionSource = null;
        }

        public override bool CanInteract() =>
            _booksReceivingInteractionsService.CanInteract(this);

        public override void Interact() =>
            _booksReceivingInteractionsService.Interact(this);
    }
}