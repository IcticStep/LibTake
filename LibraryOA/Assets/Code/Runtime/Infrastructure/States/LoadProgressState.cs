using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadProgressState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly IPersistantProgressService _persistantProgressService;
        private readonly ISaveLoadService _saveLoadService;
        private readonly IStaticDataService _staticDataService;

        public LoadProgressState(GameStateMachine stateMachine, IPersistantProgressService persistantProgressService,
            ISaveLoadService saveLoadService, IStaticDataService staticDataService)
        {
            _stateMachine = stateMachine;
            _persistantProgressService = persistantProgressService;
            _saveLoadService = saveLoadService;
            _staticDataService = staticDataService;
        }

        public void Start()
        {
            LoadProgressOrCreateNew();
            
            string startScene = _staticDataService.ScenesRouting.LevelScene;
            _stateMachine.EnterState<LoadLevelState, string>(startScene);
        }

        private void LoadProgressOrCreateNew() =>
            _persistantProgressService.Progress = 
                _saveLoadService.LoadProgress()
                ?? CreateNewProgress();

        private GameProgress CreateNewProgress()
        {
            GameProgress newProgress = new();
            newProgress.PlayerData.Lives = _staticDataService.Player.StartLivesCount;
            return newProgress;
        }

        public void Exit()
        {
        }
    }
}