using System.Threading;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.ReadBook;
using Code.Runtime.Services.Player.Lives;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class DayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly ICustomersDeliveringService _customersDeliveringService;
        private readonly IReadBookService _readBookService;
        private readonly IDaysService _daysService;
        private readonly IPlayerLivesService _playerLivesService;

        public DayState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService, 
            ICustomersDeliveringService customersDeliveringService,
            IReadBookService readBookService, IDaysService daysService,
            IPlayerLivesService playerLivesService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
            _customersDeliveringService = customersDeliveringService;
            _readBookService = readBookService;
            _daysService = daysService;
            _playerLivesService = playerLivesService;
        }

        public void Start()
        {
            _readBookService.AllowReading();
            ShowDayNumberMessage();
            ProceedDay().Forget();
        }

        public void Exit() =>
            Debug.Log($"Day {_daysService.CurrentDay} finished.");

        private void ShowDayNumberMessage()
        {
            Debug.Log($"Day {_daysService.CurrentDay} began.");
            _uiMessagesService.ShowDayMessage($"Day {_daysService.CurrentDay}");
        }

        private async UniTask ProceedDay()
        {
            CancellationTokenSource cancellationSource = new();
            
            UniTask deliverCustomersTask = _customersDeliveringService.DeliverCustomers(cancellationSource.Token);
            UniTask looseAllLives = UniTask.WaitUntil(() => _playerLivesService.Lives <= 0, 
                cancellationToken: cancellationSource.Token);

            int result = await UniTask.WhenAny(deliverCustomersTask, looseAllLives);
            cancellationSource.Cancel();
            
            switch(result)
            {
                case 0:
                    Debug.Log("All the customers have gone.");
                    _gameStateMachine.EnterState<MorningState>();
                    break;
                case 1:
                    Debug.Log("All lives are lost.");
                    _gameStateMachine.EnterState<GameOverState>();
                    break;
            }
        }
    }
}