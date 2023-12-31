using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Books.Receiving;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState
    {
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IStaticDataService _staticDataService;
        private readonly Collider _collider;
        private readonly IBookReceiver _bookReceiver;
        private readonly IProgress _progress;

        public BookReceivingState(ICustomerStateMachine customerStateMachine, IBooksReceivingService booksReceivingService, IBookReceiver bookReceiver,
            IProgress progress, IStaticDataService staticDataService, Collider collider)
        {
            _customerStateMachine = customerStateMachine;
            _booksReceivingService = booksReceivingService;
            _bookReceiver = bookReceiver;
            _progress = progress;
            _staticDataService = staticDataService;
            _collider = collider;
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

        public void Exit() =>
            _collider.enabled = false;

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
            GoToNextStateOnFinish().Forget();
        }

        private async UniTaskVoid GoToNextStateOnFinish()
        {
            int taskCompleted = await UniTask.WhenAny(_progress.Task, _bookReceiver.ReceivingTask);
            if(taskCompleted == 0)
                _customerStateMachine.Enter<PunishState>();
            if(taskCompleted == 1)
                _customerStateMachine.Enter<RewardState>();
        }
    }
}