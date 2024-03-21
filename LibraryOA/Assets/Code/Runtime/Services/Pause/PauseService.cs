using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Pause
{
    [UsedImplicitly]
    internal sealed class PauseService : IPauseService
    {
        public void Pause() =>
            Time.timeScale = 0;

        public void Resume() =>
            Time.timeScale = 1;
    }
}