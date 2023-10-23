using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public PlayerData PlayerData = new();
        public WorldData WorldData = new();
    }
}