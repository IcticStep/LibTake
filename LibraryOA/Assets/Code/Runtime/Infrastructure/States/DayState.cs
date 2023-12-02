using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Customers.Delivering;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class DayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly IPlayerProgressService _progressService;
        private readonly ICustomersDeliveringService _customersDeliveringService;

        private int CurrentDay => _progressService.Progress.WorldData.TimeData.CurrentDay;

        public DayState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService,
            IPlayerProgressService progressService, ICustomersDeliveringService customersDeliveringService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
            _progressService = progressService;
            _customersDeliveringService = customersDeliveringService;
        }

        public void Start()
        {
            ShowDayNumberMessage();
            ProceedDay().Forget();
        }

        public void Exit() { }

        private void ShowDayNumberMessage()
        {
            int day = CurrentDay;
            _uiMessagesService.ShowCenterMessage($"Day {day}");
        }

        private async UniTask ProceedDay()
        {
            await _customersDeliveringService.DeliverCustomers();
            Debug.Log("All the customers have gone.");
        }
    }
}