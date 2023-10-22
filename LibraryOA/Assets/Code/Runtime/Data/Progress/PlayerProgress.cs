using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public BookData PlayerInventory = new();
        public SkillStats SkillStats = new();
        public WorldData WorldData = new();
    }
}