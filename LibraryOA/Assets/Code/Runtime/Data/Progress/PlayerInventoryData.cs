using System;
using System.Collections.Generic;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class PlayerInventoryData
    { 
        public List<string> Books = new();
        public int Coins;
    }
}