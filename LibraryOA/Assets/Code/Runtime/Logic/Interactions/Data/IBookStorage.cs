using System;

namespace Code.Runtime.Logic.Interactions.Data
{
    public interface IBookStorage
    {
        public string CurrentBookId { get; }
        public bool HasBook { get; }
        public event Action BooksUpdated;
        public void InsertBook(string id);
        public string RemoveBook();
    }
}