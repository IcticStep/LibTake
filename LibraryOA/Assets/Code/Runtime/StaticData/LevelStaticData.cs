using System.Collections.Generic;
using Code.Runtime.StaticData.SpawnersStaticData;
using Code.Runtime.Utils;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [CreateAssetMenu(fileName = "Level", menuName = "Static data/Level")]
    public sealed class LevelStaticData : ScriptableObject
    {
        [field: ReadOnly, SerializeField]
        public string LevelKey { get; private set; }

        [field: ReadOnly, SerializeField]
        public Vector3 PlayerInitialPosition { get; private set; }

        [SerializeField] 
        private List<BookSlotSpawnData> _bookSlots;
        [SerializeField] 
        private List<ReadingTableSpawnData> _readingTables;
        [field: SerializeField]
        public TruckWayStaticData TruckWayStaticData { get; private set; }

        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        public IReadOnlyList<ReadingTableSpawnData> ReadingTables => _readingTables;
        
        public void UpdateData(string levelKey, Vector3 playerInitialPosition, List<BookSlotSpawnData> bookSlots, 
            List<ReadingTableSpawnData> readingTables, TruckWayStaticData wayStaticData)
        {
            LevelKey = levelKey;
            PlayerInitialPosition = playerInitialPosition;
            _bookSlots = bookSlots;
            _readingTables = readingTables;
            TruckWayStaticData = wayStaticData;
        }
    }
}