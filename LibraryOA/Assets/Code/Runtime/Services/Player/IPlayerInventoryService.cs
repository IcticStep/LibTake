using System;
using System.Collections.Generic;
using Code.Runtime.Infrastructure.Services.SaveLoad;
using Code.Runtime.Logic.Interactions.Data;

namespace Code.Runtime.Services.Player
{
    internal interface IPlayerInventoryService : IBookStorage, ISavedProgress
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