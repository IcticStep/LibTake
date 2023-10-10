using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Services.PlayerProvider;
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
    }
}