using System;
using System.Collections.Generic;
using Code.Runtime.StaticData.MarkersStaticData;
using UnityEngine;

namespace Code.Runtime.StaticData
{
    [Serializable]
    public sealed class InteractablesData
    {
        [SerializeField] 
        private List<BookSlotSpawnData> _bookSlots;
        [SerializeField] 
        private List<ReadingTableSpawnData> _readingTables;
        
        public IReadOnlyList<BookSlotSpawnData> BookSlots => _bookSlots;
        public IReadOnlyList<ReadingTableSpawnData> ReadingTables => _readingTables;

        public InteractablesData(List<BookSlotSpawnData> bookSlots, List<ReadingTableSpawnData> readingTables)
        {
            _bookSlots = bookSlots;
            _readingTables = readingTables;
        }
    }
}