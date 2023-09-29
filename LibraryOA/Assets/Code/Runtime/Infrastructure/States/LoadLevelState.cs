using Code.Runtime.Infrastructure.Services;
using Code.Runtime.Infrastructure.States.Api;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public async void Start(string payload) =>
            await _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded);

        private void OnLevelLoaded()
        {
            
        }

        public void Exit() =>
            throw new System.NotImplementedException();
    }
}