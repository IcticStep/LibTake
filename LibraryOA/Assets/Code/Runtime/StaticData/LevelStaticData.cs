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

        [field: ReadOnly, SerializeField]
        public Vector3 PlayerInitialPosition { get; private set; }

        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        
        public void UpdateData(string levelKey, List<BookSlotSpawnData> bookSlots, Vector3 playerInitialPosition)
        {
            LevelKey = levelKey;
            _bookSlots = bookSlots;
            PlayerInitialPosition = playerInitialPosition;
        }
    }
}