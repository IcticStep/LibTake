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

        public GameObject CreateBookSlot(string id, bool hasBook, Vector3 at, Transform parent)
        {
            GameObject bookSlot = _assetProvider.Instantiate(AssetPath.BookSlot, at, parent);
            
            Interactable interactable = bookSlot.GetComponentInChildren<Interactable>();
            interactable.InitId(id);

            BookStorage bookStorage = bookSlot.GetComponent<BookStorage>();
            bookStorage.InitHasBook(hasBook);
            
            return bookSlot;
        }

        public GameObject CreateBookSlot(Vector3 at, Transform parent = null) =>
            _assetProvider.Instantiate(AssetPath.BookSlot, at, parent);
    }
}