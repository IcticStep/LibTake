using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Services.Books.Delivering;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.TruckDriving;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class MorningGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ITruckProvider _truckProvider;
        private readonly IBooksDeliveringService _booksDeliveringService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly IReadBookService _readBookService;
        private readonly IDaysService _daysService;
        private readonly IScanBookService _scanBookService;
        private readonly ICraftingService _craftingService;

        public MorningGameState(GameStateMachine gameStateMachine, ITruckProvider truckProvider,
            IBooksDeliveringService booksDeliveringService, ISaveLoadService saveLoadService,
            IUiMessagesService uiMessagesService, IReadBookService readBookService, IDaysService daysService,
            IScanBookService scanBookService, ICraftingService craftingService, ILevelCleanUpService cleanUpService)
        {
            _gameStateMachine = gameStateMachine;
            _truckProvider = truckProvider;
            _booksDeliveringService = booksDeliveringService;
            _saveLoadService = saveLoadService;
            _uiMessagesService = uiMessagesService;
            _readBookService = readBookService;
            _daysService = daysService;
            _scanBookService = scanBookService;
            _craftingService = craftingService;
        }

        public void Start()
        {
            _readBookService.BlockReading();
            _scanBookService.BlockScanning();
            _craftingService.BlockCrafting();
            
            SaveGame();
            _daysService.AddDay();
            ShowDayNumberMessage();
            DeliverBooks().Forget();
        }

        public void Exit() { }

        private void SaveGame()
        {
            _saveLoadService.SaveProgress();
            Debug.Log($"Progress saved.");
        }

        private void ShowDayNumberMessage()
        {
            Debug.Log($"Morning {_daysService.CurrentDay}.");
            _uiMessagesService.ShowMorningMessage();
        }

        private async UniTask DeliverBooks()
        {
            _booksDeliveringService.DeliverBooksInTruck();
            
            UniTask driveTask = _truckProvider.TruckDriving.DriveToLibrary();
            UniTask booksTakenTask = _truckProvider.Truck.BooksTakenTask;

            await UniTask.WhenAll(driveTask, booksTakenTask);
            await _truckProvider.TruckDriving.DriveAwayLibrary();
            
            _gameStateMachine.EnterState<DayGameState>();
        }
    }
}