using System;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService
    {
        bool HasBook { get; }
        event Action Updated;
        void InsertBook();
        void RemoveBook();
    }
}