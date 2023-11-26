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
        
        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        public IReadOnlyList<ReadingTableSpawnData> ReadingTables => _readingTables;

        public InteractablesSpawnsData(List<BookSlotSpawnData> bookSlots, List<ReadingTableSpawnData> readingTables)
        {
            _bookSlots = bookSlots;
            _readingTables = readingTables;
        }
    }
}