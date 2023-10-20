using System;

namespace Code.Runtime.Data
{
    public interface IBookStorage
    {
        string CurrentBookId { get; }
        bool HasBook { get; }
        event Action Updated;
        void InsertBook(string id);
        string RemoveBook();
    }
}