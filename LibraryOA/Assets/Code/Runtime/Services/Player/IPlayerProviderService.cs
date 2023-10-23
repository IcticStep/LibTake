using Code.Runtime.Player;
using UnityEngine;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerProviderService
    {
        GameObject Player { get; }
        InteractablesScanner InteractablesScanner { get; }
        void RegisterPlayer(GameObject player);
        void UnregisterPlayer();
    }
}