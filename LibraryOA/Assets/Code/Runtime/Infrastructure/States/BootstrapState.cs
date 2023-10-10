using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.States.Api;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class BootstrapState : IState
    {
        private const string InitialSceneName = "Initial";
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine stateMachine, ISceneLoader sceneLoader)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Start() =>
            _sceneLoader.LoadSceneAsync(InitialSceneName, OnInitSceneLoaded).Forget();

        private void OnInitSceneLoaded() =>
            _stateMachine.EnterState<LoadProgressState>();

        public void Exit()
        {

        }
    }
}