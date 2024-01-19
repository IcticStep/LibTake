using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.StaticData.GlobalGoals;

namespace Code.Runtime.Infrastructure.States
{
    internal class WarmupState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IGlobalGoalService _globalGoalService;

        public WarmupState(GameStateMachine stateMachine, IStaticDataService staticDataService, IGlobalGoalService globalGoalService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _globalGoalService = globalGoalService;
        }

        public void Start()
        {
            WarmupServices();
            InitGlobalGoal();
            
            _stateMachine.EnterState<LoadProgressState>();
        }

        private void InitGlobalGoal()
        {
            GlobalGoal globalGoal = _staticDataService.GlobalGoals[0];
            _globalGoalService.SetGlobalGoal(globalGoal);
        }

        private void WarmupServices() =>
            _staticDataService.LoadAll();

        public void Exit()
        {
        }
    }
}