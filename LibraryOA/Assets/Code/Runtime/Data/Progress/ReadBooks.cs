using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class ReadBooks
    {
        [JsonProperty]
        private HashSet<string> _booksRead = new();

        public bool IsBookRead(string bookId) =>
            _booksRead.Contains(bookId);

        public void AddReadBook(string bookId) =>
            _booksRead.Add(bookId);
    }
}