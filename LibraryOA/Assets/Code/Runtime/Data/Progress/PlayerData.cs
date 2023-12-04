using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerData
    {
        public PlayerInventoryData Inventory = new();
        public SkillStats SkillStats = new();
        public List<string> BooksRead = new();
    }
}