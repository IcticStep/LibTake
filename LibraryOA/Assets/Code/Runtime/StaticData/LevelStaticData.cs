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
        private List<BookSlotData> _bookSlots;

        public IReadOnlyList<BookSlotData> BookSlots => _bookSlots;
        
        public void Initialize(string levelKey, List<BookSlotData> bookSlots)
        {
            LevelKey = levelKey;
            _bookSlots = bookSlots;
        }
    }
}