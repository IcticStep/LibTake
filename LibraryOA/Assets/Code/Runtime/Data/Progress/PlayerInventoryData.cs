using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class PlayerInventoryData
    {
        public Books Books = new Books();
        public Coins Coins = new Coins();
    }
}