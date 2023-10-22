using UnityEngine;

namespace Code.Runtime.Services.Player
{
    internal sealed class PlayerProviderService : IPlayerProviderService
    {
        private GameObject _player;

        public GameObject Player => _player;

        public void RegisterPlayer(GameObject player) =>
            _player = player;
        
        public void UnregisterPlayer() =>
            _player = null;
    }
}