using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.BooksDelivering;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.TruckDriving;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class MorningState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITruckProvider _truckProvider;
        private readonly IBooksDeliveringService _booksDeliveringService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IPersistantProgressService _progressService;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly IReadBookService _readBookService;

        private TimeData DaysData => _progressService.Progress.WorldData.TimeData;
        private int CurrentDay => _progressService.Progress.WorldData.TimeData.CurrentDay;

        public MorningState(GameStateMachine gameStateMachine, ITruckProvider truckProvider,
            IBooksDeliveringService booksDeliveringService, ISaveLoadService saveLoadService,
            IPersistantProgressService progressService, IUiMessagesService uiMessagesService,
            IReadBookService readBookService)
        {
            _gameStateMachine = gameStateMachine;
            _truckProvider = truckProvider;
            _booksDeliveringService = booksDeliveringService;
            _saveLoadService = saveLoadService;
            _progressService = progressService;
            _uiMessagesService = uiMessagesService;
            _readBookService = readBookService;
        }

        public void Start()
        {
            _readBookService.BlockReading();
            SaveGame();
            DaysData.AddDay();
            ShowDayNumberMessage();
            DeliverBooks().Forget();
        }

        public void Exit()
        {
            
        }

        private void SaveGame()
        {
            _saveLoadService.SaveProgress();
            Debug.Log($"Progress saved.");
        }

        private void ShowDayNumberMessage()
        {
            Debug.Log($"Morning {CurrentDay}.");
            _uiMessagesService.ShowDoubleCenterMessage($"Morning {CurrentDay}", "Books delivered!");
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