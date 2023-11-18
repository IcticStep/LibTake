using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions;
using Code.Runtime.Services.Interactions.Registry;
using Code.Runtime.StaticData;
using Code.Runtime.StaticData.SpawnersStaticData;
using JetBrains.Annotations;
using UnityEngine;

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
            GameObject bookSlot = _assetProvider.Instantiate(AssetPath.BookSlot, spawnData.Position, spawnData.TransformRotation);
            
            InitInteractable(spawnData.Id, bookSlot);
            InitBookStorage(spawnData.Id, spawnData.InitialBookId, bookSlot);

            return bookSlot;
        }

        public GameObject CreateReadingTable(string objectId, Vector3 at, Quaternion rotation, string initialBookId = null)
        {
            GameObject readingTable = _assetProvider.Instantiate(AssetPath.ReadingTable, at, rotation);
            StaticReadingTable data = _staticDataService.ReadingTableData;
            
            InitInteractable(objectId, readingTable);
            InitReadingProgress(objectId, readingTable, data);
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