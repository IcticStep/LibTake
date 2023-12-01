using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.CustomersQueue;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly BookReceiver _bookReceiver;
        private readonly Progress _progress;

        public BookReceivingState(CustomerStateMachine customerStateMachine, ICustomersQueueService customersQueueService, IBooksReceivingService booksReceivingService,
            BookReceiver bookReceiver, Progress progress)
        {
            _customerStateMachine = customerStateMachine;
            _customersQueueService = customersQueueService;
            _booksReceivingService = booksReceivingService;
            _bookReceiver = bookReceiver;
            _progress = progress;
        }

        public void Start()
        {
            string targetBook = _booksReceivingService.SelectBookForReceiving();
            _bookReceiver.Initialize(targetBook);
            _progress.Initialize(10);
            _progress.StartFilling();
            WaitForCompletion().Forget();
        }

        public void Exit() =>
            _customersQueueService.Dequeue();

        private async UniTaskVoid WaitForCompletion()
        {
            await UniTask.WhenAny(_progress.Task, _bookReceiver.ReceivingTask);
            if(_progress.Task.Status == UniTaskStatus.Succeeded)
            {
                _bookReceiver.Reset();
                Debug.Log("Receiving failed. Customer unsatisified.");
                _customerStateMachine.Enter<GoAwayState>();
            }
            else if(_bookReceiver.ReceivingTask.Status == UniTaskStatus.Succeeded)
            {
                _progress.Reset();
                Debug.Log("Receiving successful. Customer owns a book.");
                _customerStateMachine.Enter<GoAwayState>();
            }
        }
    }
}