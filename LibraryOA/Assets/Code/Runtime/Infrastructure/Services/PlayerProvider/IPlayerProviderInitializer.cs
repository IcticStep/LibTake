using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.PlayerProvider
{
    internal interface IPlayerProviderInitializer
    {
        internal void InitPlayer(GameObject player);
    }
}