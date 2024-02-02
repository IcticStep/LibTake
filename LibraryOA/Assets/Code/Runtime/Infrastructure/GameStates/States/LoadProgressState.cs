using System;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class LoadProgressState : IPayloadedState<LoadProgressOption>
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

        public void Start(LoadProgressOption payload)
        {
            switch(payload)
            {
                case LoadProgressOption.LoadProgressIfAny:
                    LoadProgressOrCreateNew();
                    break;
                case LoadProgressOption.NewGame:
                    ForceCreatingNewProgress();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(payload), payload, "Uknown progress load option.");
            }

            string startScene = _staticDataService.ScenesRouting.LevelScene;
            _stateMachine.EnterState<LoadLevelState, string>(startScene);
        }

        private void ForceCreatingNewProgress() =>
            _persistantProgressService.Progress = CreateNewProgress();

        private void LoadProgressOrCreateNew() =>
            _persistantProgressService.Progress =
                TryLoadProgress(out GameProgress progress)
                    ? progress
                    : CreateNewProgress();

        private bool TryLoadProgress(out GameProgress progress)
        {
            progress = _saveLoadService.LoadProgress();
            return progress is not null;
        }

        private GameProgress CreateNewProgress()
        {
            GameProgress newProgress = new();
            newProgress.PlayerData.Lives = _staticDataService.Player.StartLivesCount;
            return newProgress;
        }

        public void Exit() { }
    }
}