using UnityEngine;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerProviderService
    {
        GameObject Player { get; }
        void RegisterPlayer(GameObject player);
        void UnregisterPlayer();
    }
}