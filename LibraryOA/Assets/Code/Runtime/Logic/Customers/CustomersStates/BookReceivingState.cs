using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic.Customers.CustomersStates.Api;
using Code.Runtime.Services.Books.Receiving;
using Code.Runtime.StaticData.Balance;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Logic.Customers.CustomersStates
{
    internal class BookReceivingState : ICustomerState, IForceStoppable
    {
        private readonly ICustomerStateMachine _customerStateMachine;
        private readonly IBooksReceivingService _booksReceivingService;
        private readonly IStaticDataService _staticDataService;
        private readonly Collider _collider;
        private readonly IBookReceiver _bookReceiver;
        private readonly IProgress _progress;

        private UniTaskCompletionSource _forceStopCompletionSource;

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

        public void Exit()
        {
            if(_collider != null)
                _collider.enabled = false;
        }

        private void InitializeReceiving()
        {
            _forceStopCompletionSource = new UniTaskCompletionSource();
            string targetBook = _booksReceivingService.SelectBookForReceiving();
            _bookReceiver.Initialize(targetBook);
            float timeToReceiveBook = GetTimeToReceiveBook();
            _progress.Initialize(timeToReceiveBook);
            Debug.Log($"Customer is receiving book. Time to receive: {timeToReceiveBook}. Calculated based on books count: {_booksReceivingService.BooksInLibrary}.");
            _collider.enabled = true;
        }

        private void StartReceivingProgress()
        {
            _progress.StartFilling();
            GoToNextStateOnFinish().Forget();
        }

        public void ForceStop()
        {
            _progress.StopFilling();
            _forceStopCompletionSource.TrySetResult();
        }

        private async UniTaskVoid GoToNextStateOnFinish()
        {
            int taskCompleted = await UniTask.WhenAny(
                _progress.Task, 
                _bookReceiver.ReceivingTask, 
                _forceStopCompletionSource.Task);
            
            switch(taskCompleted)
            {
                case 0:
                    _customerStateMachine.Enter<PunishState>();
                    break;
                case 1:
                    _customerStateMachine.Enter<RewardState>();
                    break;
                case 2:
                    _customerStateMachine.Enter<GoAwayState>();
                    break;
            }
        }

        private float GetTimeToReceiveBook()
        {
            ReceivingTimeSettings timeSettings = _staticDataService.BookReceiving.ReceivingTimeSettings;
            
            float timeToReceiveBook = timeSettings.TimeToReceiveBook;
            int booksCount = _booksReceivingService.BooksInLibrary;
            float additionalTime = GetAdditionalBookReceivingTime(timeSettings, booksCount);
            return timeToReceiveBook + additionalTime;
        }

        private static float GetAdditionalBookReceivingTime(ReceivingTimeSettings timeSettings, int booksCount)
        {
            if(booksCount <= timeSettings.NotAffectedByAdditionalTimeBooksCount)
                return 0;
            
            return timeSettings.AdditionalTimePerBook * (booksCount - timeSettings.NotAffectedByAdditionalTimeBooksCount);
        }
    }
}