using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.StaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IGameFactory _gameFactory;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IPlayerProgressService _playerProgress;

        private string _levelName;
        private LevelStaticData _levelData;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IGameFactory gameFactory,
            IStaticDataService staticData, ISaveLoadRegistry saveLoadRegistry, IPlayerProgressService playerProgress)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _gameFactory = gameFactory;
            _staticData = staticData;
            _saveLoadRegistry = saveLoadRegistry;
            _playerProgress = playerProgress;
        }

        public void Start(string payload)
        {
            _levelName = payload;
            _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded).Forget();
            _levelData = _staticData.ForLevel(_levelName);
        }

        public void Exit()
        {
            
        }

        private void OnLevelLoaded()
        {
            InitGameWorld();
            InformProgressReaders();
            
            _stateMachine.EnterState<GameLoopState>();
        }

        private void InformProgressReaders()
        {
            foreach(ISavedProgressReader progressReader in _saveLoadRegistry.ProgressReaders)
                progressReader.LoadProgress(_playerProgress.Progress);
        }

        private void InitGameWorld()
        {
            InitPlayer();
            InitBookSlots();
        }

        private GameObject InitPlayer() =>
            _gameFactory.CreatePlayer(_levelData.PlayerInitialPosition);

        private void InitBookSlots()
        {
            foreach(BookSlotSpawnData spawn in _levelData.BookSlots)
            {
                _gameFactory.CreateBookSlot(spawn.SlotId, spawn.Position, spawn.InitialBookId);
            }
        }
    }
}