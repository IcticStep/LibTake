using System;

namespace Code.Runtime.Data
{
    [Serializable]
    public sealed class PlayerProgress
    {
        public BookSlot PlayerInventory = new();
        public SkillStats SkillStats = new();
    }
}