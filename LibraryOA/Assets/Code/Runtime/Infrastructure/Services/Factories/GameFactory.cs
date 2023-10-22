using Code.Runtime.Data;
using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Logic;
using Code.Runtime.Logic.Interactions;
using Code.Runtime.Services.Player;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    internal sealed class GameFactory : IGameFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerProviderInitializer _playerProviderInitializer;

        public GameFactory(IAssetProvider assetProvider, IPlayerProviderInitializer playerProviderInitializer)
        {
            _assetProvider = assetProvider;
            _playerProviderInitializer = playerProviderInitializer;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            GameObject player = _assetProvider.Instantiate(AssetPath.Player, at);
            _playerProviderInitializer.InitPlayer(player);
            return player;
        }
        
        public GameObject CreateBookSlot(string bookSlotId, Vector3 at, string initialBookId = null)
        {
            GameObject bookSlot = _assetProvider.Instantiate(AssetPath.BookSlot, at);
            
            Interactable interactable = bookSlot.GetComponentInChildren<Interactable>();
            interactable.InitId(bookSlotId);

            if(!string.IsNullOrWhiteSpace(initialBookId))
            {
                BookStorageHolder bookStorageHolder = bookSlot.GetComponentInChildren<BookStorageHolder>();
                bookStorageHolder.Initialize(bookSlotId, initialBookId);
            }

            return bookSlot;
        }

        public GameObject CreateBookSlot(Vector3 at, Transform parent = null) =>
            _assetProvider.Instantiate(AssetPath.BookSlot, at, parent);
    }
}