using System;
using UnityEngine.Serialization;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerData
    {
        public PlayerInventoryData Inventory = new();
        public SkillStats SkillStats = new();
        public ReadBooks ReadBooks = new();
    }
}