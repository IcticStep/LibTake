using System;

namespace Code.Runtime.Data
{
    internal sealed class BookStorage : IBookStorage
    {
        public string CurrentBookId { get; private set; }
        public bool HasBook => !string.IsNullOrWhiteSpace(CurrentBookId);

        public event Action Updated;

        public void InsertBook(string id)
        {
            if(HasBook)
                throw new InvalidOperationException("Can't insert more than one book into storage!");

            CurrentBookId = id;
            Updated?.Invoke();
        }

        public string RemoveBook()
        {
            if(!HasBook)
                throw new InvalidOperationException("Can't remove book from storage when empty!");

            string removed = CurrentBookId;
            CurrentBookId = string.Empty;
            Updated?.Invoke();
            
            return removed;
        }
    }
}