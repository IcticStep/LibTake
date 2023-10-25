using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Interactions;
using Code.Runtime.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories.Interactables
{
    [UsedImplicitly]
    internal sealed class ReadingTableFactory : IReadingTableFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;
        private readonly IStaticDataService _staticDataService;
        private readonly IInteractablesRegistry _interactablesRegistry;

        public ReadingTableFactory(IAssetProvider assetProvider, ISaveLoadRegistry saveLoadRegistry, 
            IStaticDataService staticDataService, IInteractablesRegistry interactablesRegistry)
        {
            _assetProvider = assetProvider;
            _saveLoadRegistry = saveLoadRegistry;
            _staticDataService = staticDataService;
            _interactablesRegistry = interactablesRegistry;
        }
        
        public GameObject Create(string objectId, Vector3 at, string initialBookId = null)
        {
            GameObject readingTable = Instantiate(at);
            StaticReadingTable data = _staticDataService.ReadingTableData;
            
            InitInteractable(objectId, readingTable);
            InitProgress(objectId, readingTable, data);
            InitBookStorage(objectId, initialBookId, readingTable);

            return readingTable;
        }

        private void InitProgress(string objectId, GameObject readingTable, StaticReadingTable data)
        {
            Progress progress = readingTable
                .GetComponentInChildren<Progress>();

            progress.Initialize(objectId, data.SecondsToRead);
            _saveLoadRegistry.Register(progress);
        }

        private GameObject Instantiate(Vector3 at) =>
            _assetProvider.Instantiate(AssetPath.ReadingTable, at);

        private void InitInteractable(string objectId, GameObject readingTable)
        {
            Interactable interactable = readingTable.GetComponentInChildren<Interactable>();
            Collider collider = interactable.GetComponent<Collider>();
            interactable.InitId(objectId);
            _interactablesRegistry.Register(interactable, collider);
        }

        private void InitBookStorage(string objectId, string initialBookId, GameObject readingTable)
        {
            BookStorageHolder bookStorage = readingTable.GetComponentInChildren<BookStorageHolder>();
            _saveLoadRegistry.Register(bookStorage);
            bookStorage.Initialize(objectId, initialBookId);
        }
    }
}