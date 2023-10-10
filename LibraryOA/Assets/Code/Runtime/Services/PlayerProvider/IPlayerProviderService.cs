using UnityEngine;

namespace Code.Runtime.Services.PlayerProvider
{
    internal interface IPlayerProviderService
    {
        GameObject Player { get; }
    }
}