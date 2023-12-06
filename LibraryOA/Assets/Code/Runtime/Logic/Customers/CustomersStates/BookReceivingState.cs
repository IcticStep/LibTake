using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.BooksReceiving;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.Services.Player;
using Code.Runtime.Services.Player.Inventory;
using Code.Runtime.Services.Player.Lives;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly CustomerStateMachine _customerStateMachine;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IStaticDataService _staticDataService;
        private readonly Collider _collider;
        private readonly IPlayerInventoryService _playerInventoryService;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly BookReceiver _bookReceiver;
        private readonly Progress _progress;

        public BookReceivingState(CustomerStateMachine customerStateMachine, ICustomersQueueService customersQueueService, IBooksReceivingService booksReceivingService,
            BookReceiver bookReceiver, Progress progress, IStaticDataService staticDataService, Collider collider,
            IPlayerInventoryService playerInventoryService, IPlayerLivesService playerLivesService)
        {
            _customerStateMachine = customerStateMachine;
            _customersQueueService = customersQueueService;
            _booksReceivingService = booksReceivingService;
            _bookReceiver = bookReceiver;
            _progress = progress;
            _staticDataService = staticDataService;
            _collider = collider;
            _playerInventoryService = playerInventoryService;
            _playerLivesService = playerLivesService;
        }

        public void Start()
        {
            if(!_booksReceivingService.LibraryHasBooks)
            {
                Debug.Log("There is no books. Customer is leaving.");
                _customerStateMachine.Enter<GoAwayState>();
                return;
            }
            
            InitializeReceiving();
            StartReceivingProgress();
        }

        public void Exit()
        {
            _customersQueueService.Dequeue();
            _collider.enabled = false;
        }

        private void InitializeReceiving()
        {
            string targetBook = _booksReceivingService.SelectBookForReceiving();
            _bookReceiver.Initialize(targetBook);
            _progress.Initialize(_staticDataService.BookReceiving.TimeToReceiveBook);
            _collider.enabled = true;
        }

        private void StartReceivingProgress()
        {
            _progress.StartFilling();
            WaitForCompletion().Forget();
        }

        private async UniTaskVoid WaitForCompletion()
        {
            int taskCompleted = await UniTask.WhenAny(_progress.Task, _bookReceiver.ReceivingTask);
            if(taskCompleted == 0)
                OnTimeOut();
            else if(taskCompleted == 1)
                OnBookReceived();
        }

        private void OnTimeOut()
        {
            _bookReceiver.Reset();
            _progress.Reset();
            Debug.Log("Receiving failed. Customer unsatisified.");
            _playerLivesService.WasteLife();
            _customerStateMachine.Enter<GoAwayState>();
        }

        private void OnBookReceived()
        {
            _progress.Reset();
            _playerInventoryService.AddCoins(_staticDataService.BookReceiving.BookReceivedReward);
            Debug.Log("Receiving successful. Customer owns a book.");
            _customerStateMachine.Enter<GoAwayState>();
        }
    }
}