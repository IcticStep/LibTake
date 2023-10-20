using System.Collections.Generic;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Level", menuName = "Static data/Level")]
    public sealed class LevelStaticData : ScriptableObject
    {
        [field: ReadOnly, SerializeField]
        public string LevelKey { get; private set; }

        [SerializeField] 
        private List<BookSlotSpawnData> _bookSlots;

        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        
        public void Initialize(string levelKey, List<BookSlotSpawnData> bookSlots)
        {
            LevelKey = levelKey;
            _bookSlots = bookSlots;
        }
    }
}