using Code.Runtime.Logic.Player;
using UnityEngine;

namespace Code.Runtime.Services.Player.Provider
{
    internal interface IPlayerProviderService
    {
        GameObject Player { get; }
        InteractablesScanner InteractablesScanner { get; }
        PlayerInteractor PlayerInteractor { get; }
        Transform Transform { get; }
        void RegisterPlayer(GameObject player);
        void CleanUp();
    }
}