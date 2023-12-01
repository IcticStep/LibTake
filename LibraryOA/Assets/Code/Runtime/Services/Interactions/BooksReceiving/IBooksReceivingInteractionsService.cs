using Code.Runtime.Logic.Customers;

namespace Code.Runtime.Services.Interactions.BooksReceiving
{
    internal interface IBooksReceivingInteractionsService
    {
        bool CanInteract(BookReceiver bookReceiver);
        void Interact(BookReceiver bookReceiver);
    }
}