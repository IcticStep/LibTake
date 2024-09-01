using System;
using System.Threading;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.Crafting;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Interactions.Scanning;
using Code.Runtime.Services.Player.Lives;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class DayGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly ICustomersDeliveringService _customersDeliveringService;
        private readonly IReadBookService _readBookService;
        private readonly IDaysService _daysService;
        private readonly IPlayerLivesService _playerLivesService;
        private readonly IScanBookService _scanBookService;
        private readonly ICraftingService _craftingService;
        private readonly ILevelCleanUpService _levelCleanUpService;

        private UniTaskCompletionSource _forceExitCompletionSource;

        public DayGameState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService, 
            ICustomersDeliveringService customersDeliveringService, IReadBookService readBookService, IDaysService daysService,
            IPlayerLivesService playerLivesService, IScanBookService scanBookService, ICraftingService craftingService,
            ILevelCleanUpService levelCleanUpService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
            _customersDeliveringService = customersDeliveringService;
            _readBookService = readBookService;
            _daysService = daysService;
            _playerLivesService = playerLivesService;
            _scanBookService = scanBookService;
            _craftingService = craftingService;
            _levelCleanUpService = levelCleanUpService;
        }

        public void Start()
        {
            _forceExitCompletionSource = new UniTaskCompletionSource();
            _readBookService.AllowReading();
            _scanBookService.AllowScanning();
            _craftingService.AllowCrafting();
            ShowDayNumberMessage();
            ProceedDay().Forget();
        }

        public void Exit()
        {
            _forceExitCompletionSource.TrySetResult();
            Debug.Log($"Day {_daysService.CurrentDay} finished.");
        }

        private void ShowDayNumberMessage()
        {
            Debug.Log($"Day {_daysService.CurrentDay} began.");
            _uiMessagesService.ShowDayMessage();
        }

        private async UniTask ProceedDay()
        {
            using CancellationTokenSource cancellationSource = CancellationTokenSource.CreateLinkedTokenSource(_levelCleanUpService.RestartCancellationToken);
            
            UniTask deliverCustomersTask = _customersDeliveringService.DeliverCustomers(cancellationSource.Token);
            UniTask looseAllLives = UniTask.WaitUntil(() => _playerLivesService.Lives <= 0, 
                cancellationToken: cancellationSource.Token);

            int result = -1;
            try
            {
                result = await UniTask.WhenAny(deliverCustomersTask, looseAllLives, _forceExitCompletionSource.Task);
                cancellationSource.Cancel();
            }
            catch(OperationCanceledException)
            {
                // ignored
            }
            finally
            {
                switch(result)
                {
                    case 0:
                        Debug.Log("All the customers have gone.");
                        _gameStateMachine.EnterState<MorningGameState>();
                        break;
                    case 1:
                        Debug.Log("All lives are lost.");
                        _gameStateMachine.EnterState<GameOverGameState>();
                        break;
                    case 2:
                    case -1:
                        break;
                }
            }
        }
    }
}