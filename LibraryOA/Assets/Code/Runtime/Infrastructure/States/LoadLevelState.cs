using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.States.Api;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
        }

        public void Start(string payload) =>
            _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded).Forget();

        public void Exit()
        {
            
        }

        private void OnLevelLoaded()
        {
            InitGameWorld();
            _stateMachine.EnterState<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitPlayer();
            InitBookSlots();
        }

        // TODO: refactor finding initial point to something clever
        private GameObject InitPlayer() =>
            _gameFactory.CreatePlayer(GameObject.FindWithTag("PlayerInitialPoint").transform.position);

        private void InitBookSlots()
        {
        }
    }
}