using Code.Runtime.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player
{
    [UsedImplicitly]
    internal sealed class PlayerProviderService : IPlayerProviderService
    {
        public GameObject Player { get; private set; }
        public InteractablesScanner InteractablesScanner { get; private set; }

        public void RegisterPlayer(GameObject player)
        {
            Player = player;
            InteractablesScanner = Player.GetComponent<InteractablesScanner>();
        }

        public void UnregisterPlayer()
        {
            Player = null;
            InteractablesScanner = null;
        }
    }
}