using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Loading;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class RestartGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public RestartGameState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService, ISceneLoader sceneLoader,
            IStaticDataService staticDataService, ILoadingCurtainService loadingCurtainService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _loadingCurtainService = loadingCurtainService;
        }
        
        public void Start()
        {
            _saveLoadService.DeleteProgress();
            _gameStateMachine.EnterState<MenuGameState>();
            
            GoToMainMenu()
                .Forget();
        }

        private async UniTaskVoid GoToMainMenu()
        {
            await _loadingCurtainService.ShowBlackAsync();
            await _sceneLoader.LoadSceneAsync(_staticDataService.ScenesRouting.MenuScene);
        }

        public void Exit() { }
    }
}