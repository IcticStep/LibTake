using System;
using Cysharp.Threading.Tasks;

namespace Code.Runtime.Logic.Customers
{
    public interface IBookReceiver
    {
        string BookId { get; }
        bool Received { get; }
        UniTask ReceivingTask { get; }
        event Action Updated;
        void Reset();
        void Initialize(string bookIdRequested);
        void ReceiveBook(string bookId);
        bool CanInteract();
        void Interact();
    }
}