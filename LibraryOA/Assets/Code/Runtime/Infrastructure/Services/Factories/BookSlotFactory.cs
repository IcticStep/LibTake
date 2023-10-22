using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    [UsedImplicitly]
    internal sealed class BookSlotFactory : IBookSlotFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public BookSlotFactory(IAssetProvider assetProvider, ISaveLoadRegistry saveLoadRegistry)
        {
            _assetProvider = assetProvider;
            _saveLoadRegistry = saveLoadRegistry;
        }
        
        public GameObject Create(string bookSlotId, Vector3 at, string initialBookId = null)
        {
            GameObject bookSlot = Instantiate(at);
            
            InitInteractable(bookSlotId, bookSlot);
            InitBookStorage(bookSlotId, initialBookId, bookSlot);

            return bookSlot;
        }

        private GameObject Instantiate(Vector3 at) =>
            _assetProvider.Instantiate(AssetPath.BookSlot, at);

        private static void InitInteractable(string bookSlotId, GameObject bookSlot)
        {
            Interactable interactable = bookSlot.GetComponentInChildren<Interactable>();
            interactable.InitId(bookSlotId);
        }

        private void InitBookStorage(string bookSlotId, string initialBookId, GameObject bookSlot)
        {
            BookStorageHolder bookStorage = bookSlot.GetComponentInChildren<BookStorageHolder>();
            _saveLoadRegistry.Register(bookStorage);
            bookStorage.Initialize(bookSlotId, initialBookId);
        }
    }
}