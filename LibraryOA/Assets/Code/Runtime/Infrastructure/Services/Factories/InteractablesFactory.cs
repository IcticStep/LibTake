using System.Linq;
using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.Interactables;
using Code.Runtime.StaticData.Level.MarkersStaticData;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Splines;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    [UsedImplicitly]
    internal sealed class InteractablesFactory : IInteractablesFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IInteractablesRegistry _interactablesRegistry;
        private readonly IStaticDataService _staticDataService;

        public InteractablesFactory(IAssetProvider assetProvider, ISaveLoadRegistry saveLoadRegistry,
            IInteractablesRegistry interactablesRegistry, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _saveLoadRegistry = saveLoadRegistry;
            _interactablesRegistry = interactablesRegistry;
            _staticDataService = staticDataService;
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

        private void InitInteractable(string id, GameObject bookSlot)
        {
            Interactable interactable = bookSlot.GetComponentInChildren<Interactable>();
            Collider collider = interactable.GetComponent<Collider>();
            interactable.InitId(id);
            _interactablesRegistry.Register(interactable, collider);
        }

        private void InitBookStorage(string id, string initialBookId, GameObject bookSlot)
        {
            BookStorageHolder bookStorage = bookSlot.GetComponentInChildren<BookStorageHolder>();
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