using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Days;
using Code.Runtime.Services.Interactions.ReadBook;
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

        public DayState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService, ICustomersDeliveringService customersDeliveringService,
            IReadBookService readBookService, IDaysService daysService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
            _customersDeliveringService = customersDeliveringService;
            _readBookService = readBookService;
            _daysService = daysService;
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
            _uiMessagesService.ShowCenterMessage($"Day {_daysService.CurrentDay}");
        }

        private async UniTask ProceedDay()
        {
            await _customersDeliveringService.DeliverCustomers();
            Debug.Log("All the customers have gone.");
            _gameStateMachine.EnterState<MorningState>();
        }
    }
}