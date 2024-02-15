using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Services.Loading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    [UsedImplicitly]
    internal sealed class GameOverGameState : IGameState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelCleanUpService _levelCleanUpService;
        private readonly GameStateMachine _gameStateMachine;
        private readonly ILoadingCurtainService _loadingCurtainService;

        public GameOverGameState(ISceneLoader sceneLoader, IStaticDataService staticDataService, ILevelCleanUpService levelCleanUpService,
            GameStateMachine gameStateMachine, ILoadingCurtainService loadingCurtainService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _levelCleanUpService = levelCleanUpService;
            _gameStateMachine = gameStateMachine;
            _loadingCurtainService = loadingCurtainService;
        }

        public void Start()
        {
            _gameStateMachine.EnterState<MenuGameState>();

            GoToGameOverScene().Forget();
        }

        private async UniTaskVoid GoToGameOverScene()
        {
            string gameOverScene = _staticDataService.ScenesRouting.GameOverScene;
            await _loadingCurtainService.ShowBlackAsync();
            await _sceneLoader.LoadSceneAsync(gameOverScene, OnSceneLoaded);
            await _loadingCurtainService.HideBlackAsync();
        }

        private void OnSceneLoaded() =>
            _levelCleanUpService.CleanUp();

        public void Exit() { }
    }
}