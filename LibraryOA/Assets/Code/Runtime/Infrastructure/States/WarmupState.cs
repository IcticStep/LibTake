using System;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal class WarmupState : IState
    {
        private readonly GameStateMachine _stateMachine;

        public WarmupState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Start()
        {
            WarmupServices();
            
            _stateMachine.EnterState<LoadProgressState>();
        }

        private void WarmupServices()
        {
            
        }

        public void Exit()
        {
        }
    }
}