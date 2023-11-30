namespace Code.Runtime.Services.BooksReceiving
{
    internal interface IBooksReceivingService
    {
        string SelectBookForReceiving();
        void ReceiveBook(string book);
    }
}