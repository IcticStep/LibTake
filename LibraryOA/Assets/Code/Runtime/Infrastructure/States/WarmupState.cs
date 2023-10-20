using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal class WarmupState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;

        public WarmupState(GameStateMachine stateMachine, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
        }

        public void Start()
        {
            WarmupServices();
            
            _stateMachine.EnterState<LoadProgressState>();
        }

        private void WarmupServices() =>
            _staticDataService.LoadAll();

        public void Exit()
        {
        }
    }
}