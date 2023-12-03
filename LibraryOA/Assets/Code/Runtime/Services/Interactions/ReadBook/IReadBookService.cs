namespace Code.Runtime.Services.Interactions.ReadBook
{
    internal interface IReadBookService
    {
        void ReadBook(string bookId);
        bool IsRead(string bookId);
    }
}