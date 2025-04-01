using Code.Runtime.Infrastructure.AssetManagement;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Player;
using Code.Runtime.Services.Player.Provider;
using Code.Runtime.StaticData.CharacterSelection;
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
        private readonly ISaveLoadService _saveLoadService;

        public CharactersFactory(
            IAssetProvider assetProvider, 
            IPlayerProviderService playerProvider,
            ISaveLoadRegistry saveLoadRegistry,
            ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _assetProvider = assetProvider;
            _playerProvider = playerProvider;
            _saveLoadRegistry = saveLoadRegistry;
        }

        public GameObject CreatePlayer(Vector3 at)
        {
            GameObject player = _assetProvider.Instantiate(AssetPath.Player, at);
            
            CharacterTypeId characterSelected = _saveLoadService.LoadCharacterSelected();
            player.GetComponentInChildren<PlayerView>().SetCharacter(characterSelected);
            
            _playerProvider.RegisterPlayer(player);
            _saveLoadRegistry.RegisterAllComponents(player);
            return player;
        }
    }
}