using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerData
    {
        public BookData BookInHands = new();
        public SkillStats SkillStats = new();
        public ReadBooks ReadBooks = new();
    }
}