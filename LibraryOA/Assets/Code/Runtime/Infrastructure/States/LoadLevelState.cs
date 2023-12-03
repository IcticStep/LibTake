using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Customers.Delivering;
using Code.Runtime.Services.Customers.Queue;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Level;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Code.Runtime.Infrastructure.States
{
    internal sealed class LoadLevelState : IPayloadedState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly ISceneLoader _sceneLoader;
        private readonly IInteractablesFactory _interactablesFactory;
        private readonly IStaticDataService _staticData;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IPlayerProgressService _playerProgress;
        private readonly ICharactersFactory _charactersFactory;
        private readonly IHudFactory _hudFactory;
        private readonly ICustomersQueueService _customersQueueService;
        private readonly ICustomersDeliveringService _customersDeliveringService;
        
        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData,
            ISaveLoadRegistry saveLoadRegistry, IPlayerProgressService playerProgress, IInteractablesFactory interactablesFactory,
            ICharactersFactory charactersFactory, IHudFactory hudFactory, ICustomersQueueService customersQueueService,
            ICustomersDeliveringService customersDeliveringService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _interactablesFactory = interactablesFactory;
            _staticData = staticData;
            _saveLoadRegistry = saveLoadRegistry;
            _playerProgress = playerProgress;
            _charactersFactory = charactersFactory;
            _hudFactory = hudFactory;
            _customersQueueService = customersQueueService;
            _customersDeliveringService = customersDeliveringService;
        }

        public void Start(string payload) =>
            _sceneLoader.LoadSceneAsync(payload, OnLevelLoaded).Forget();

        public void Exit()
        {
            
        }

        private void OnLevelLoaded()
        {
            LevelStaticData levelData = _staticData.CurrentLevelData;
            GameObject player = InitPlayer(levelData);
            InitGameWorld(levelData);
            InformProgressReaders();
            InitCamera(player);
            InitUi();
            
            _stateMachine.EnterState<MorningState>();
        }

        private void InitGameWorld(LevelStaticData levelData)
        {
            InitBookSlots(levelData);
            InitReadingTables(levelData);
            InitTruck(levelData);
            InitCustomers(levelData);
        }

        private GameObject InitPlayer(LevelStaticData levelData) =>
            _charactersFactory.CreatePlayer(levelData.PlayerInitialPosition);

        private void InitUi() =>
            _hudFactory.Create();

        private void InitBookSlots(LevelStaticData levelData)
        {
            foreach(BookSlotSpawnData spawn in levelData.InteractablesSpawns.BookSlots)
                _interactablesFactory.CreateBookSlot(spawn);
        }

        private void InitReadingTables(LevelStaticData levelData)
        {
            foreach(ReadingTableSpawnData readingTable in levelData.InteractablesSpawns.ReadingTables)
                _interactablesFactory.CreateReadingTable(readingTable.Id, readingTable.Position, readingTable.Rotation, readingTable.InitialBookId);
        }

        private void InitCustomers(LevelStaticData levelData)
        {
            _customersQueueService.Initialize(levelData.Customers.QueuePoints);
            _customersDeliveringService.CreateCustomers();
        }

        private void InitTruck(LevelStaticData levelData) =>
            _interactablesFactory.CreateTruck(levelData.TruckWay);

        private void InitCamera(GameObject player) =>
            Camera.main!
                .GetComponent<CameraFollow>()
                .SetTarget(player.transform);

        private void InformProgressReaders()
        {
            foreach(ISavedProgressReader progressReader in _saveLoadRegistry.ProgressReaders)
                progressReader.LoadProgress(_playerProgress.Progress);
        }
    }
}