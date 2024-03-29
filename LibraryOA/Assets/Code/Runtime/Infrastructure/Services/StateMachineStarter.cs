using Code.Runtime.Infrastructure.GameStates;
using Code.Runtime.Infrastructure.GameStates.States;
using JetBrains.Annotations;
using Zenject;

namespace Code.Runtime.Infrastructure.Services
{
    [UsedImplicitly]
    internal sealed class StateMachineStarter : IInitializable
    {
        private readonly GameStateMachine _stateMachine;

        public StateMachineStarter(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Initialize() =>
            _stateMachine.EnterState<BootstrapGameState>();
    }
}