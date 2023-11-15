using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadProgressState : IState
    {
        private const string MainSceneName = "Library";
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine stateMachine, IPlayerProgressService playerProgressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _playerProgressService = playerProgressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Start()
        {
            LoadProgressOrCreateNew();
            
            string startScene = _staticDataService.StartupSettings.StartScene;
            _stateMachine.EnterState<LoadLevelState, string>(startScene);
        }

        private void LoadProgressOrCreateNew() =>
            _playerProgressService.Progress = 
                _saveLoadService.LoadProgress()
                ?? CreateNewProgress();

        private PlayerProgress CreateNewProgress() =>
            new();

        public void Exit()
        {
        }
    }
}