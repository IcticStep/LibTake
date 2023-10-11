using System;

namespace Code.Runtime.Services.Player
{
    internal sealed class PlayerInventoryService : IPlayerInventoryService
    {
        public bool HasBook { get; private set; }

        public event Action Updated;

        public void InsertBook()
        {
            if(HasBook)
                throw new InvalidOperationException("Can't insert more than one book into player inventory!");

            HasBook = true;
            Updated?.Invoke();
        }

        public void RemoveBook()
        {
            if(!HasBook)
                throw new InvalidOperationException("Can't remove book from player inventory when empty!");

            HasBook = false;
            Updated?.Invoke();
        }
    }
}