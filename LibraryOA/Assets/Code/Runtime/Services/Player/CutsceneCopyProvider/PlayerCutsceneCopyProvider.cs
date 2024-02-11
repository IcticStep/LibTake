using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player.CutsceneCopyProvider
{
    [UsedImplicitly]
    internal sealed class PlayerCutsceneCopyProvider : IPlayerCutsceneCopyProvider
    {
        public GameObject GameObject { get; private set; }
        
        public void RegisterPlayer(GameObject player) =>
            GameObject = player;
        
        public void CleanUp() =>
            GameObject = null;
    }
}