using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public class PlayerInventoryData
    {
        [JsonProperty]
        private Stack<string> _books = new();

        public event Action Updated;

        public int Count => _books.Count;
        public IEnumerable<string> AllBooks => _books;

        public void Push(string bookId)
        {
            _books.Push(bookId);
            Updated?.Invoke();
        }

        public string Pop()
        {
            string bookId = _books.Pop();
            Updated?.Invoke();
            return bookId;
        }

        public string Peek()
        {
            string bookId = _books.Peek();
            Updated?.Invoke();
            return bookId;
        }
    }
}