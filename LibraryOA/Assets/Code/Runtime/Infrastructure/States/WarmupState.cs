using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.Services.Loading;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States
{
    internal class WarmupState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IGlobalGoalService _globalGoalService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public WarmupState(GameStateMachine stateMachine, IStaticDataService staticDataService, IGlobalGoalService globalGoalService,
            ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtainService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _globalGoalService = globalGoalService;
            _sceneLoader = sceneLoader;
            _loadingCurtainService = loadingCurtainService;
        }

        public void Start()
        {
            WarmupServices();
            InitGlobalGoal();
            
            GoToMenu().Forget();
        }

        private void InitGlobalGoal()
        {
            GlobalGoal globalGoal = _staticDataService.GlobalGoals[0];
            _globalGoalService.SetGlobalGoal(globalGoal);
        }

        private void WarmupServices() =>
            _staticDataService.LoadAll();

        private async UniTaskVoid GoToMenu()
        {
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);
            _loadingCurtainService.Hide();
            _stateMachine.EnterState<MenuState>();
        }

        public void Exit()
        {
        }
    }
}