using Code.Runtime.Infrastructure.Services.Factories;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.SceneMenegment;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Infrastructure.States.Api;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.CustomersQueue;
using Code.Runtime.Services.TruckDriving;
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
        private readonly ITruckProvider _truckProvider;
        private readonly ICustomersQueueService _customersQueueService;

        private string _levelName;
        private LevelStaticData _levelData;

        public LoadLevelState(GameStateMachine stateMachine, ISceneLoader sceneLoader, IStaticDataService staticData,
            ISaveLoadRegistry saveLoadRegistry, IPlayerProgressService playerProgress, IInteractablesFactory interactablesFactory,
            ICharactersFactory charactersFactory, IHudFactory hudFactory, ITruckProvider truckProvider,
            ICustomersQueueService customersQueueService)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _interactablesFactory = interactablesFactory;
            _staticData = staticData;
            _saveLoadRegistry = saveLoadRegistry;
            _playerProgress = playerProgress;
            _charactersFactory = charactersFactory;
            _hudFactory = hudFactory;
            _truckProvider = truckProvider;
            _customersQueueService = customersQueueService;
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
            GameObject player = InitPlayer();
            InitGameWorld();
            InformProgressReaders();
            InitCamera(player);
            InitUi();
            
            _stateMachine.EnterState<MorningState>();
        }

        private void InitGameWorld()
        {
            InitBookSlots();
            InitReadingTables();
            InitTruck();
            InitCustomersQueue();
        }

        private GameObject InitPlayer() =>
            _charactersFactory.CreatePlayer(_levelData.PlayerInitialPosition);

        private void InitUi() =>
            _hudFactory.Create();

        private void InitBookSlots()
        {
            foreach(BookSlotSpawnData spawn in _levelData.InteractablesSpawns.BookSlots)
                _interactablesFactory.CreateBookSlot(spawn);
        }

        private void InitReadingTables()
        {
            foreach(ReadingTableSpawnData readingTable in _levelData.InteractablesSpawns.ReadingTables)
                _interactablesFactory.CreateReadingTable(readingTable.Id, readingTable.Position, readingTable.Rotation, readingTable.InitialBookId);
        }

        private void InitCustomersQueue() =>
            _customersQueueService.Initialize(_levelData.Customers.QueuePoints);

        private void InitTruck()
        {
            GameObject truck = _interactablesFactory.CreateTruck(_levelData.TruckWay);
        }

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