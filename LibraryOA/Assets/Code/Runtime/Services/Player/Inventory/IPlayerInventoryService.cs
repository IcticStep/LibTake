using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactables.Data;

namespace Code.Runtime.Services.Player.Inventory
{
    public interface IPlayerInventoryService : IBookStorage, ISavedProgress
    {
        int Coins { get; }
        int BooksCount { get; }
        IReadOnlyList<string> Books { get; }
        event Action CoinsUpdated;
        void InsertBooks(IEnumerable<string> bookIds);
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        void CleanUp();
    }
}