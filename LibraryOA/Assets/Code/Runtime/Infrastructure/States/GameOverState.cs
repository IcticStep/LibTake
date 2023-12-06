using Code.Runtime.Infrastructure.Services.CleanUp;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.States
{
    [UsedImplicitly]
    internal sealed class GameOverState : IState
    {
        private readonly ISceneLoader _sceneLoader;
        private readonly IStaticDataService _staticDataService;
        private readonly ILevelCleanUpService _levelCleanUpService;

        public GameOverState(
            ISceneLoader sceneLoader, 
            IStaticDataService staticDataService,
            ILevelCleanUpService levelCleanUpService)
        {
            _sceneLoader = sceneLoader;
            _staticDataService = staticDataService;
            _levelCleanUpService = levelCleanUpService;
        }

        public void Start()
        {
            string gameOverScene = _staticDataService.ScenesRouting.GameOverScene;
            _sceneLoader.LoadSceneAsync(gameOverScene, OnSceneLoaded);
        }

        private void OnSceneLoaded() =>
            _levelCleanUpService.CleanUp();

        public void Exit()
        {
        }
    }
}