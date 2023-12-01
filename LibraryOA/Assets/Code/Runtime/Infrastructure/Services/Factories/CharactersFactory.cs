using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Services.Player;
using Code.Runtime.StaticData;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.Factories
{
    [UsedImplicitly]
    internal sealed class CharactersFactory : ICharactersFactory
    {
        private readonly IAssetProvider _assetProvider;
        private readonly IPlayerProviderService _playerProvider;
        private readonly ISaveLoadRegistry _saveLoadRegistry;

        public CharactersFactory(IAssetProvider assetProvider, IPlayerProviderService playerProvider, ISaveLoadRegistry saveLoadRegistry)
        {
            _assetProvider = assetProvider;
            _playerProvider = playerProvider;
            _saveLoadRegistry = saveLoadRegistry;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            GameObject player = _assetProvider.Instantiate(AssetPath.Player, at);
            _playerProvider.RegisterPlayer(player);
            _saveLoadRegistry.RegisterAllComponents(player);
            return player;
        }
    }
}