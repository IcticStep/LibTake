namespace Code.Runtime.Services.BooksReceiving
{
    internal interface IBooksReceivingService
    {
        string SelectBookForReceiving();
        void ReceiveBook(string book);
        bool LibraryHasBooks { get; }
        int BooksInLibrary { get; }
    }
}