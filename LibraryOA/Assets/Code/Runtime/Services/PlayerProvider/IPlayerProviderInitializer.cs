using UnityEngine;

namespace Code.Runtime.Services.PlayerProvider
{
    internal interface IPlayerProviderInitializer
    {
        internal void InitPlayer(GameObject player);
    }
}