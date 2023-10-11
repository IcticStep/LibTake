using UnityEngine;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerProviderInitializer
    {
        internal void InitPlayer(GameObject player);
    }
}