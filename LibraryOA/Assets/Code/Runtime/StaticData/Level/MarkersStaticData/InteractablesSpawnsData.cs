using System;
using System.Collections.Generic;
using UnityEngine;

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
        
        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        public IReadOnlyList<ReadingTableSpawnData> ReadingTables => _readingTables;
        public IReadOnlyList<ScannerSpawnData> Scanners => _scanners;

        public InteractablesSpawnsData(List<BookSlotSpawnData> bookSlots, List<ReadingTableSpawnData> readingTables, List<ScannerSpawnData> scanners)
        {
            _bookSlots = bookSlots;
            _readingTables = readingTables;
            _scanners = scanners;
        }
    }
}