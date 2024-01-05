using System;

namespace Code.Runtime.StaticData.Interactables
{
    [Serializable]
    public sealed class InteractablesStaticData
    {
        public StaticScanner Scanner { get; private set; }
        public StaticReadingTable ReadingTable { get; private set; }
        public StaticBookSlot BookSlot { get; private set; }
        public StaticTruck Truck { get; private set; }

        public InteractablesStaticData(StaticReadingTable readingTable, StaticBookSlot bookSlot, StaticTruck truck)
        {
            ReadingTable = readingTable;
            BookSlot = bookSlot;
            Truck = truck;
        }
    }
}