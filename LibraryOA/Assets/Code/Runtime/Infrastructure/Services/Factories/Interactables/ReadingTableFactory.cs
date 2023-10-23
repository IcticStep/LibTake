using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Infrastructure.Services.StaticData;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
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

        public ReadingTableFactory(IAssetProvider assetProvider, ISaveLoadRegistry saveLoadRegistry, IStaticDataService staticDataService)
        {
            _assetProvider = assetProvider;
            _saveLoadRegistry = saveLoadRegistry;
            _staticDataService = staticDataService;
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

        private void InitProgress(string objectId, GameObject readingTable, StaticReadingTable data) =>
            readingTable
                .GetComponentInChildren<Progress>()
                .Initialize(objectId, data.SecondsToRead);

        private GameObject Instantiate(Vector3 at) =>
            _assetProvider.Instantiate(AssetPath.ReadingTable, at);

        private static void InitInteractable(string objectId, GameObject readingTable)
        {
            Interactable interactable = readingTable.GetComponentInChildren<Interactable>();
            interactable.InitId(objectId);
        }

        private void InitBookStorage(string objectId, string initialBookId, GameObject readingTable)
        {
            BookStorageHolder bookStorage = readingTable.GetComponentInChildren<BookStorageHolder>();
            _saveLoadRegistry.Register(bookStorage);
            bookStorage.Initialize(objectId, initialBookId);
        }
    }
}