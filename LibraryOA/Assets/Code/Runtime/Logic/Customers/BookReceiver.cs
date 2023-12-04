using System;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.BooksReceiving;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Code.Runtime.Logic.Customers
{
    public class BookReceiver : Interactable
    {
        [FormerlySerializedAs("_bookStorageHolder")]
        [SerializeField]
        private BookStorage _bookStorage;

        private UniTaskCompletionSource _taskCompletionSource;
        private IBooksReceivingInteractionsService _booksReceivingInteractionsService;
        
        public string BookId { get; private set; }
        public bool Received { get; private set; }
        public UniTask ReceivingTask => _taskCompletionSource.Task;

        public event Action Updated;

        [Inject]
        private void Construct(IBooksReceivingInteractionsService booksReceivingInteractionsService) =>
            _booksReceivingInteractionsService = booksReceivingInteractionsService;

        public void Reset()
        {
            BookId = null;
            _taskCompletionSource = null;
            Received = false;
            Updated?.Invoke();
        }

        public void Initialize(string bookIdRequested)
        {
            _taskCompletionSource = new UniTaskCompletionSource();
            BookId = bookIdRequested;
            Updated?.Invoke();
        }

        public void ReceiveBook(string bookId)
        {
            _bookStorage.InsertBook(bookId);
            Received = true;
            _taskCompletionSource.TrySetResult();
            Updated?.Invoke();
        }

        public override bool CanInteract() =>
            _booksReceivingInteractionsService.CanInteract(this);

        public override void Interact() =>
            _booksReceivingInteractionsService.Interact(this);
    }
}