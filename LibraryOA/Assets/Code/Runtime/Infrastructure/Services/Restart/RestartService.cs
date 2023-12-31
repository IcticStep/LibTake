using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.States;
using JetBrains.Annotations;

namespace Code.Runtime.Infrastructure.Services.Restart
{
    [UsedImplicitly]
    internal sealed class RestartService : IRestartService
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public RestartService(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }

        public void Restart()
        {
            _saveLoadService.DeleteProgress();
            _gameStateMachine.EnterState<LoadProgressState>();
        }
    }
}