using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class Books
    {
        [JsonProperty]
        private List<string> _books = new();
        
        public event Action Updated;

        [JsonIgnore]
        public int Count => _books.Count;

        [JsonIgnore]
        public IReadOnlyList<string> AllBooks => _books;

        public void Push(string bookId)
        {
            _books.Add(bookId);
            Updated?.Invoke();
        }

        public string Pop()
        {
            string bookId = _books[^1];
            _books.RemoveAt(_books.Count - 1);
            Updated?.Invoke();
            return bookId;
        }

        public string Peek() =>
            _books[^1];
    }
}