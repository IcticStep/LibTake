namespace Code.Runtime.Services.Interactions.ReadBook
{
    internal interface IReadBookService
    {
        bool ReadingAllowed { get; }
        void AllowReading();
        void BlockReading();
        void ReadBook(string bookId);
        bool CanReadBook(string bookId);
        bool IsRead(string bookId);
    }
}