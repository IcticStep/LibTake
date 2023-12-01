using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class DayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiMessagesService _uiMessagesService;
        private readonly IPlayerProgressService _progressService;
        
        private int CurrentDay => _progressService.Progress.WorldData.TimeData.CurrentDay;

        public DayState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService,
            IPlayerProgressService progressService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
            _progressService = progressService;
        }

        public void Start()
        {
            ShowDayNumberMessage();
            Debug.Log("Day started.");
        }

        public void Exit() { }

        private void ShowDayNumberMessage()
        {
            int day = CurrentDay;
            _uiMessagesService.ShowCenterMessage($"Day {day}");
        }
    }
}