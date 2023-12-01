using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.BooksDelivering;
using Code.Runtime.Services.TruckDriving;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class MorningState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITruckProvider _truckProvider;
        private readonly IBooksDeliveringService _booksDeliveringService;
        private readonly ISaveLoadService _saveLoadService;

        public MorningState(GameStateMachine gameStateMachine, ITruckProvider truckProvider,
            IBooksDeliveringService booksDeliveringService, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _truckProvider = truckProvider;
            _booksDeliveringService = booksDeliveringService;
            _saveLoadService = saveLoadService;
        }

        public void Start()
        {
            _saveLoadService.SaveProgress();
            DeliverBooks().Forget();
        }

        public void Exit()
        {
            
        }

        private async UniTask DeliverBooks()
        {
            _booksDeliveringService.DeliverBooksInTruck();
            
            UniTask driveTask = _truckProvider.TruckDriving.DriveToLibrary();
            UniTask booksTakenTask = _truckProvider.Truck.BooksTakenTask;

            await UniTask.WhenAll(driveTask, booksTakenTask);
            await _truckProvider.TruckDriving.DriveAwayLibrary();
            
            _gameStateMachine.EnterState<DayState>();
        }
    }
}