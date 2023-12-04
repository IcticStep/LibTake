using System;
using System.Collections.Generic;
using System.Linq;
using Code.Runtime.Data;
using Code.Runtime.Data.Progress;
using Code.Runtime.Infrastructure.Services.PersistentProgress;
using Code.Runtime.Logic.Interactions.Data;
using JetBrains.Annotations;
using UnityEngine;
using Zenject;

namespace Code.Runtime.Services.Player
{
    [UsedImplicitly]
    internal sealed class PlayerInventoryService : IPlayerInventoryService
    {
        private IPlayerProgressService _progressService;

        public string CurrentBookId => HasBook ? Inventory.Books.Peek() : null;
        public int Count => Inventory.Books.Count;
        public bool HasBook => Count > 0;
        public IReadOnlyList<string> Books => Inventory.Books.AllBooks;
        public int Coins => Inventory.Coins.Amount;

        private PlayerInventoryData Inventory => _progressService.Progress.PlayerData.PlayerInventory;

        public event Action BooksUpdated;
        public event Action CoinsUpdated;

        [Inject]
        private void Construct(IPlayerProgressService progressService) =>
            _progressService = progressService;

        public void InsertBooks(IEnumerable<string> bookIds)
        {
            foreach(string bookId in bookIds)
                InsertBook(bookId);
        }

        public void InsertBook(string id)
        {
            Inventory.Books.Push(id);
            BooksUpdated?.Invoke();
        }

        public string RemoveBook()
        {
            string removedId = Inventory.Books.Pop();
            BooksUpdated?.Invoke();
            return removedId;
        }
        
        public void AddCoins(int amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException($"Tried to add {amount} coins. Can't add coins amount less then zero!");

            Inventory.Coins.Amount += amount;
            CoinsUpdated?.Invoke();
            Debug.Log($"Coins amount: {Coins}.");
        }
        
        public void RemoveCoins(int amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException($"Tried to remove {amount} coins. Can't remove coins amount less then zero!");

            Inventory.Coins.Amount -= amount;
            CoinsUpdated?.Invoke();         
            Debug.Log($"Coins amount: {Coins}.");
        }

        public void LoadProgress(PlayerProgress progress) =>
            BooksUpdated?.Invoke();
    }
}