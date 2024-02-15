using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
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

        public GameOverGameState(ISceneLoader sceneLoader, IStaticDataService staticDataService, ILevelCleanUpService levelCleanUpService,
            GameStateMachine gameStateMachine)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _levelCleanUpService = levelCleanUpService;
            _gameStateMachine = gameStateMachine;
        }

        public void Start()
        {
            _gameStateMachine.EnterState<MenuGameState>();
            
            string gameOverScene = _staticDataService.ScenesRouting.GameOverScene;
            _sceneLoader.LoadSceneAsync(gameOverScene, OnSceneLoaded);
        }

        private void OnSceneLoaded() =>
            _levelCleanUpService.CleanUp();

        public void Exit() { }
    }
}