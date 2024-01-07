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
        public StaticStatue Statue { get; }

        public InteractablesStaticData(StaticReadingTable readingTable, StaticBookSlot bookSlot, StaticTruck truck,
            StaticScanner scanner, StaticStatue statue)
        {
            Statue = statue;
            ReadingTable = readingTable;
            BookSlot = bookSlot;
            Truck = truck;
            Scanner = scanner;
        }
    }
}