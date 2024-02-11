using UnityEngine;

namespace Code.Runtime.Services.Player.CutsceneCopyProvider
{
    internal interface IPlayerCutsceneCopyProvider
    {
        GameObject GameObject { get; }
        void RegisterPlayer(GameObject player);
        void CleanUp();
    }
}