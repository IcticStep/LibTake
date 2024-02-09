using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.Locales;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.GlobalGoals;
using Code.Runtime.Services.Loading;
using Code.Runtime.StaticData.GlobalGoals;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal class WarmupGameState : IGameState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IStaticDataService _staticDataService;
        private readonly IGlobalGoalService _globalGoalService;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILoadingCurtainService _loadingCurtainService;
        private readonly ILocalizationService _localizationService;

        public WarmupGameState(GameStateMachine stateMachine, IStaticDataService staticDataService, IGlobalGoalService globalGoalService,
            ISceneLoader sceneLoader, ILoadingCurtainService loadingCurtainService, ILocalizationService localizationService)
        {
            _stateMachine = stateMachine;
            _staticDataService = staticDataService;
            _globalGoalService = globalGoalService;
            _sceneLoader = sceneLoader;
            _loadingCurtainService = loadingCurtainService;
            _localizationService = localizationService;
        }

        public void Start() =>
            WarmupServices()
                .Forget();

        private async UniTaskVoid WarmupServices()
        {
            _staticDataService.LoadAll();
            await _localizationService.WarmUp();
            await GoToMenu();
        }

        private async UniTask GoToMenu()
        {
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);
            _loadingCurtainService.HideImageAsync().Forget();
            _stateMachine.EnterState<MenuGameState>();
        }

        public void Exit() { }
    }
}