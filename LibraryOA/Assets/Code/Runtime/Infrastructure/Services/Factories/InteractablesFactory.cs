using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Customers;
using Code.Runtime.Logic.Customers.CustomersStates;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.Services.TruckDriving;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    [UsedImplicitly]
    public sealed class InteractablesFactory : IInteractablesFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IInteractablesRegistry _interactablesRegistry;
        private readonly IStaticDataService _staticDataService;
        private readonly UniqueIdUpdater _uniqueIdUpdater = new();
        private readonly ITruckProvider _truckProvider;

        public InteractablesFactory(IAssetProvider assetProvider, ISaveLoadRegistry saveLoadRegistry,
            IInteractablesRegistry interactablesRegistry, IStaticDataService staticDataService, ITruckProvider truckProvider)
        {
            _assetProvider = assetProvider;
            _saveLoadRegistry = saveLoadRegistry;
            _interactablesRegistry = interactablesRegistry;
            _staticDataService = staticDataService;
            _truckProvider = truckProvider;
        }
        
        public GameObject CreateBookSlot(BookSlotSpawnData spawnData)
        {
            StaticBookSlot staticData = _staticDataService.Interactables.BookSlot;
            GameObject bookSlot = _assetProvider.Instantiate(staticData.Prefab, spawnData.Position, spawnData.TransformRotation);

            InitInteractable(spawnData.Id, bookSlot);
            InitBookStorage(spawnData.Id, spawnData.InitialBookId, bookSlot);

            return bookSlot;
        }
        
        public GameObject CreateTruck(TruckWayStaticData truckWayData)
        {
            StaticTruck staticData = _staticDataService.Interactables.Truck;
            GameObject truck = _assetProvider.Instantiate(staticData.Prefab, truckWayData.HiddenPoint.Position, truckWayData.HiddenPoint.Rotation);
            
            string id = truck.GetComponentInChildren<UniqueId>().Id;
            InitInteractable(id, truck);
            
            _truckProvider.RegisterTruck(truck);

            return truck;
        }

        public GameObject CreateReadingTable(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null)
        {
            StaticReadingTable staticData = _staticDataService.Interactables.ReadingTable;
            GameObject readingTable = _assetProvider.Instantiate(staticData.Prefab, at, rotation);

            InitInteractable(objectId, readingTable);
            InitReadingProgress(objectId, readingTable, staticData);
            InitBookStorage(objectId, initialBookId, readingTable);

            return readingTable;
        }
        
        public CustomerStateMachine CreateCustomer(Vector3 at)
        {
            GameObject customer = _assetProvider.Instantiate(AssetPath.Customer, at);

            UniqueId uniqueId = customer.GetComponentInChildren<UniqueId>();
            _uniqueIdUpdater.UpdateUniqueId(uniqueId);
            
            InitInteractable(uniqueId.Id, customer);

            CustomerStateMachine stateMachine = customer.GetComponent<CustomerStateMachine>();
            stateMachine.Enter<DeactivatedState>();
            
            return stateMachine;
        }

        private void InitInteractable(string id, GameObject gameObject)
        {
            Interactable interactable = gameObject.GetComponentInChildren<Interactable>();
            Collider collider = interactable.GetComponent<Collider>();
            interactable.InitId(id);
            _interactablesRegistry.Register(interactable, collider);
        }

        private void InitBookStorage(string id, string initialBookId, GameObject gameObject)
        {
            BookStorageHolder bookStorage = gameObject.GetComponentInChildren<BookStorageHolder>();
            _saveLoadRegistry.Register(bookStorage);
            bookStorage.Initialize(id, initialBookId);
        }
        
        private void InitReadingProgress(string objectId, GameObject gameObject, StaticReadingTable data)
        {
            Progress progress = gameObject.GetComponentInChildren<Progress>();
            progress.Initialize(objectId, data.SecondsToRead);
            _saveLoadRegistry.Register(progress);
        }
    }
}