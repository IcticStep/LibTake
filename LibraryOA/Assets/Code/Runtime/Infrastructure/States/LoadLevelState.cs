using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.Factories.Interactables;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Services.Player;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.SpawnersStaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IBookSlotFactory _bookSlotFactory;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IPlayerProgressService _playerProgress;
        private readonly IPlayerInventoryService _playerInventory;
        private readonly IPlayerFactory _playerFactory;
        private readonly IReadingTableFactory _readingTableFactory;

        private string _levelName;
        private LevelStaticData _levelData;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData,
            ISaveLoadRegistry saveLoadRegistry, IPlayerProgressService playerProgress, IPlayerInventoryService playerInventory,
            IBookSlotFactory bookSlotFactory, IPlayerFactory playerFactory, IReadingTableFactory readingTableFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _bookSlotFactory = bookSlotFactory;
            _staticData = staticData;
            _saveLoadRegistry = saveLoadRegistry;
            _playerProgress = playerProgress;
            _playerInventory = playerInventory;
            _playerFactory = playerFactory;
            _readingTableFactory = readingTableFactory;
        }

        public void Start(string payload)
        {
            _levelName = payload;
            _levelData = _staticData.ForLevel(_levelName);
            _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded).Forget();
        }

        public void Exit()
        {
            
        }

        private void OnLevelLoaded()
        {
            InitPlayer();
            InitGameWorld();
            InformProgressReaders();
            
            _stateMachine.EnterState<GameLoopState>();
        }

        private void InitGameWorld()
        {
            InitBookSlots();
            InitReadingTables();
        }

        private GameObject InitPlayer()
        {
            GameObject player = _playerFactory.Create(_levelData.PlayerInitialPosition);
            _saveLoadRegistry.Register(_playerInventory);
            return player;
        }

        private void InitBookSlots()
        {
            foreach(BookSlotSpawnData spawn in _levelData.BookSlots)
            {
                _bookSlotFactory.Create(spawn.Id, spawn.Position, spawn.InitialBookId);
            }
        }
        
        private void InitReadingTables()
        {
            foreach(ReadingTableSpawnData readingTable in _levelData.ReadingTables)
            {
                _readingTableFactory.Create(readingTable.Id, readingTable.Position, readingTable.InitialBookId);
            }
        }

        private void InformProgressReaders()
        {
            foreach(ISavedProgressReader progressReader in _saveLoadRegistry.ProgressReaders)
                progressReader.LoadProgress(_playerProgress.Progress);
        }
    }
}