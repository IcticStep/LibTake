using Code.Runtime.Data;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadProgressState : IState
    {
        private const string MainSceneName = "Library";
        
        private readonly GameStateMachine _stateMachine;
        private readonly IPlayerProgressService _playerProgressService;
        private readonly ISaveLoadService _saveLoadService;

        public LoadProgressState(GameStateMachine stateMachine, IPlayerProgressService playerProgressService, ISaveLoadService saveLoadService)
        {
            _stateMachine = stateMachine;
            _playerProgressService = playerProgressService;
            _saveLoadService = saveLoadService;
        }

        public void Start()
        {
            LoadProgressOrCreateNew();
            _stateMachine.EnterState<LoadLevelState, string>(MainSceneName);
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