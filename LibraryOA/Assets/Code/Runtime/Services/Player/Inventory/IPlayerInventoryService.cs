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
        bool HasBook { get; }
        int BooksCount { get; }
        string CurrentBookId { get; }
        IReadOnlyList<string> Books { get; }
        event Action BooksUpdated;
        event Action CoinsUpdated;
        void InsertBooks(IEnumerable<string> bookIds);
        void InsertBook(string id);
        string RemoveBook();
        void AddCoins(int amount);
        void RemoveCoins(int amount);
        void LoadProgress(GameProgress progress);
        void UpdateProgress(GameProgress progress);
        void CleanUp();
    }
}