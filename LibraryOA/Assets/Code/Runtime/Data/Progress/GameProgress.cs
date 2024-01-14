using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class GameProgress
    {
        public PlayerData PlayerData = new();
        public WorldData WorldData = new();
    }
}