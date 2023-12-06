using Code.Runtime.Logic.Player;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player.Provider
{
    [UsedImplicitly]
    internal sealed class PlayerProviderService : IPlayerProviderService
    {
        public GameObject Player { get; private set; }
        public InteractablesScanner InteractablesScanner { get; private set; }
        public PlayerInteractor PlayerInteractor { get; private set; }

        public void RegisterPlayer(GameObject player)
        {
            Player = player;
            InteractablesScanner = Player.GetComponent<InteractablesScanner>();
            PlayerInteractor = Player.GetComponentInChildren<PlayerInteractor>();
        }

        public void CleanUp()
        {
            Player = null;
            InteractablesScanner = null;
            PlayerInteractor = null;
        }
    }
}