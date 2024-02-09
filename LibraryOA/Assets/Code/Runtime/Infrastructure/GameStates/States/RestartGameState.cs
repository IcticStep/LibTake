using Code.Runtime.Data;
using Code.Runtime.Infrastructure.GameStates.Api;
using Code.Runtime.Infrastructure.Services.SaveLoad;

namespace Code.Runtime.Infrastructure.GameStates.States
{
    internal sealed class RestartGameState : IGameState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly ISaveLoadService _saveLoadService;

        public RestartGameState(GameStateMachine gameStateMachine, ISaveLoadService saveLoadService)
        {
            _gameStateMachine = gameStateMachine;
            _saveLoadService = saveLoadService;
        }
        
        public void Start()
        {
            _saveLoadService.DeleteProgress();
            _gameStateMachine.EnterState<LoadProgressState, LoadProgressOption>(LoadProgressOption.NewGame);
        }

        public void Exit() { }
    }
}