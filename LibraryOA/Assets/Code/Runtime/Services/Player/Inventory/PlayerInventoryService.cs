using System;
using System.Collections.Generic;
using Code.Runtime.Data.Progress;
using JetBrains.Annotations;
using UnityEngine;

namespace Code.Runtime.Services.Player.Inventory
{
    [UsedImplicitly]
    internal sealed class PlayerInventoryService : IPlayerInventoryService
    {
        private List<string> _books = new();

        public int Coins { get; private set; } = 0;
        public bool HasBook => BooksCount > 0;
        public int BooksCount => _books.Count;
        public string CurrentBookId => HasBook ? _books[^1] : null;
        public IReadOnlyList<string> Books => _books;

        public event Action BooksUpdated;
        public event Action CoinsUpdated;
        public event Action AllBooksRemoved;
        
        public void InsertBooks(IEnumerable<string> bookIds)
        {
            foreach(string bookId in bookIds)
                InsertBook(bookId);
        }

        public void InsertBook(string id)
        {
            _books.Add(id);
            BooksUpdated?.Invoke();
        }

        public string RemoveBook()
        {
            string removedId = _books[^1];
            _books.RemoveAt(_books.Count-1);
            BooksUpdated?.Invoke();
            if(_books.Count == 0)
                AllBooksRemoved?.Invoke();
            
            return removedId;
        }
        
        public void AddCoins(int amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException($"Tried to add {amount} coins. Can't add coins amount less then zero!");

            Coins += amount;
            CoinsUpdated?.Invoke();
            Debug.Log($"Coins amount: {Coins}.");
        }
        
        public void RemoveCoins(int amount)
        {
            if(amount < 0)
                throw new ArgumentOutOfRangeException($"Tried to remove {amount} coins. Can't remove coins amount less then zero!");

            Coins -= amount;
            CoinsUpdated?.Invoke();         
            Debug.Log($"Coins amount: {Coins}.");
        }

        public void LoadProgress(GameProgress progress)
        {
            _books = progress.PlayerData.Inventory.Books;
            BooksUpdated?.Invoke();
            Coins = progress.PlayerData.Inventory.Coins;
            CoinsUpdated?.Invoke();
        }

        public void UpdateProgress(GameProgress progress)
        {
            progress.PlayerData.Inventory.Books = _books;
            progress.PlayerData.Inventory.Coins = Coins;
        }

        public void CleanUp()
        {
            _books.Clear();
            Coins = 0;
        }
    }
}