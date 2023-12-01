using Code.Runtime.Infrastructure.Services.UiMessages;
using Code.Runtime.Infrastructure.States.Api;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class DayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly IUiMessagesService _uiMessagesService;

        public DayState(GameStateMachine gameStateMachine, IUiMessagesService uiMessagesService)
        {
            _gameStateMachine = gameStateMachine;
            _uiMessagesService = uiMessagesService;
        }

        public void Start()
        {
            _uiMessagesService.ShowCenterMessage("Day 1");
            Debug.Log("Day started.");
        }

        public void Exit() { }
    }
}