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
        public StaticStatue Statue { get; private set; }
        public StaticCraftingTable CraftingTable { get; private set; }

        public InteractablesStaticData(StaticReadingTable readingTable, StaticBookSlot bookSlot, StaticTruck truck,
            StaticScanner scanner, StaticStatue statue, StaticCraftingTable craftingTable)
        {
            Statue = statue;
            CraftingTable = craftingTable;
            ReadingTable = readingTable;
            BookSlot = bookSlot;
            Truck = truck;
            Scanner = scanner;
        }
    }
}