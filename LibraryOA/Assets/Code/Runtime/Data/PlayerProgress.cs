using System;

namespace Code.Runtime.Data
{
    [Serializable]
    internal sealed class PlayerProgress
    {
        public BookSlot PlayerInventory;

        public PlayerProgress()
        {
            PlayerInventory = new BookSlot();
        }
    }
}