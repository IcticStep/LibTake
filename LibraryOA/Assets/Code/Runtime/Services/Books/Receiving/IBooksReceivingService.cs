namespace Code.Runtime.Services.Books.Receiving
{
    internal interface IBooksReceivingService
    {
        string SelectBookForReceiving();
        void ReceiveBook(string book);
        bool LibraryHasBooks { get; }
        int BooksInLibrary { get; }
    }
}