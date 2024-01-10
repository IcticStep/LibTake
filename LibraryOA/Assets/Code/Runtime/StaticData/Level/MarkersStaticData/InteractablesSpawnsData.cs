using System;
using System.Collections.Generic;
using Code.Runtime.Logic.Markers.Spawns;
using UnityEngine;
using UnityEngine.Serialization;

namespace Code.Runtime.StaticData.Level.MarkersStaticData
{
    [Serializable]
    public sealed class InteractablesSpawnsData
    {
        [SerializeField] 
        private List<BookSlotSpawnData> _bookSlots;
        [SerializeField] 
        private List<ReadingTableSpawnData> _readingTables;
        [SerializeField]
        private List<ScannerSpawnData> _scanners;
        [SerializeField]
        private List<StatueSpawnData> _statues;
        [SerializeField]
        private List<CraftingTableSpawnData> _craftingTables;

        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        public IReadOnlyList<ReadingTableSpawnData> ReadingTables => _readingTables;
        public IReadOnlyList<ScannerSpawnData> Scanners => _scanners;
        public IReadOnlyList<StatueSpawnData> Statues => _statues;
        public IReadOnlyList<CraftingTableSpawnData> CraftingTables => _craftingTables;

        public InteractablesSpawnsData(List<BookSlotSpawnData> bookSlots, List<ReadingTableSpawnData> readingTables, List<ScannerSpawnData> scanners,
            List<StatueSpawnData> statues, List<CraftingTableSpawnData> craftingTables)
        {
            _bookSlots = bookSlots;
            _readingTables = readingTables;
            _scanners = scanners;
            _statues = statues;
            _craftingTables = craftingTables;
        }
    }
}