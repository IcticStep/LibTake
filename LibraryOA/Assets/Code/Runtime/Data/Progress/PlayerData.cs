using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerData
    {
        public PlayerInventoryData PlayerInventory = new();
        public SkillStats SkillStats = new();
        public ReadBooks ReadBooks = new();
    }
}