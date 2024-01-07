using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactables.Data;

namespace Code.Runtime.Services.Player.Inventory
{
    public interface IPlayerInventoryService : IBookStorage, ISavedProgress
    {
        int BooksCount { get; }
        IReadOnlyList<string> Books { get; }
        int Coins { get; }
        void InsertBooks(IEnumerable<string> bookIds);
        event Action CoinsUpdated;
        void AddCoins(int amount);
        void RemoveCoins(int amount);
    }
}