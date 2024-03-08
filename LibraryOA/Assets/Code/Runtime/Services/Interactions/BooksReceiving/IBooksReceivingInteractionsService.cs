using System;
using Code.Runtime.Logic.Customers;

namespace Code.Runtime.Services.Interactions.BooksReceiving
{
    internal interface IBooksReceivingInteractionsService
    {
        bool CanInteract(IBookReceiver bookReceiver);
        void Interact(IBookReceiver bookReceiver);
        event Action BooksReceived;
    }
}