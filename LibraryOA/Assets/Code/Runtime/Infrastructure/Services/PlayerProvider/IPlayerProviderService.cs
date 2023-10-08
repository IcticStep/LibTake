using UnityEngine;

namespace Code.Runtime.Infrastructure.Services.PlayerProvider
{
    internal interface IPlayerProviderService
    {
        GameObject Player { get; }
    }
}