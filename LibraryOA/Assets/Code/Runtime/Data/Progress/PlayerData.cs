using System;
using System.Collections.Generic;
using Code.Runtime.StaticData.Books;
using UnityEngine.Serialization;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class PlayerData
    {
        public PlayerInventoryData Inventory = new();
        public Dictionary<BookType, int> Skills = new();
        public List<string> BooksRead = new();
        public List<string> BooksScanned = new();
        public int Lives;
    }
}