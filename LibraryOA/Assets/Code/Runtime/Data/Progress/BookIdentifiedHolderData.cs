using System;

namespace Code.Runtime.Data.Progress
{
    [Serializable]
    public sealed class BookIdentifiedHolderData
    {
        public string Id;
        public BookData BookData;

        public BookIdentifiedHolderData(string id, BookData bookData)
        {
            Id = id;
            BookData = bookData;
        }
    }
}