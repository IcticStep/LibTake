using UnityEngine;

namespace Code.Runtime.Services.Player
{
    internal sealed class PlayerProviderService : IPlayerProviderService, IPlayerProviderInitializer
    {
        private GameObject _player;

        public GameObject Player => _player;

        void IPlayerProviderInitializer.InitPlayer(GameObject player) =>
            _player = player;
    }
}