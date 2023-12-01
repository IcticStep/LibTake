using Code.Runtime.Infrastructure.States.Api;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class DayState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public DayState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Exit() { }

        public void Start()
        {
            Debug.Log("Day started.");
        }
    }
}