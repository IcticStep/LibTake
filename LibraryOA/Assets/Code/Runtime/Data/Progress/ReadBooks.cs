using System;
using System.Collections.Generic;
using UnityEngine;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class ReadBooks
    {
        [SerializeField]
        private List<string> _booksRead = new();

        public bool IsBookRead(string bookId) =>
            _booksRead.Contains(bookId);

        public void AddReadBook(string bookId) =>
            _booksRead.Add(bookId);
    }
}