using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class BookData
    {
        public string BookId;

        public BookData()
        {
            BookId = string.Empty;
        }

        public BookData(string bookId)
        {
            BookId = bookId;
        }
    }
}